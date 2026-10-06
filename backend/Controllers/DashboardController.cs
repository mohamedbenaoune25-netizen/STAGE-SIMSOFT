using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public class TimeSeriesData
        {
            public string Date { get; set; } = string.Empty;
            public int Count { get; set; }
        }

        public class AdvancedDashboardStats
        {
            public int TotalUsers { get; set; }
            public int TotalBlogs { get; set; }
            public int TotalRequests { get; set; }
            public int PendingRequests { get; set; }
            
            public List<TimeSeriesData> RequestsTrend { get; set; } = new();
            public List<TimeSeriesData> BlogsTrend { get; set; } = new();

            public Dictionary<string, int> RequestsByStatus { get; set; } = new();
            public Dictionary<string, int> BlogsByCategory { get; set; } = new();

            public List<VisitorRequest> RecentRequests { get; set; } = new();
        }

        // GET: api/dashboard/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats([FromQuery] int days = 30)
        {
            var startDate = DateTime.UtcNow.AddDays(-days);

            // Basic KPIs
            var totalUsers = await _context.Users.CountAsync();
            var totalBlogs = await _context.BlogPosts.CountAsync();
            var totalRequests = await _context.VisitorRequests.CountAsync();
            var pendingRequests = await _context.VisitorRequests.CountAsync(r => r.Status == "Nouveau");

            // Time series (Area Chart)
            // Group requests by date (ignoring time)
            var requestsTrendData = await _context.VisitorRequests
                .Where(r => r.CreatedAt >= startDate)
                .GroupBy(r => r.CreatedAt.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            var blogsTrendData = await _context.BlogPosts
                .Where(b => b.CreatedAt >= startDate)
                .GroupBy(b => b.CreatedAt.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            // Fill missing dates
            var requestsTrend = new List<TimeSeriesData>();
            var blogsTrend = new List<TimeSeriesData>();
            for (int i = days - 1; i >= 0; i--)
            {
                var d = DateTime.UtcNow.Date.AddDays(-i);
                var rMatch = requestsTrendData.FirstOrDefault(x => x.Date == d);
                var bMatch = blogsTrendData.FirstOrDefault(x => x.Date == d);
                
                requestsTrend.Add(new TimeSeriesData { Date = d.ToString("yyyy-MM-dd"), Count = rMatch?.Count ?? 0 });
                blogsTrend.Add(new TimeSeriesData { Date = d.ToString("yyyy-MM-dd"), Count = bMatch?.Count ?? 0 });
            }

            // Donut Chart: Request Status Distribution
            var requestsByStatusList = await _context.VisitorRequests
                .GroupBy(r => r.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();
            
            var requestsByStatus = requestsByStatusList.ToDictionary(x => x.Status ?? "Inconnu", x => x.Count);

            // Horizontal Bar Chart: Posts by Category
            var allCategories = new[] { 
                "ERP Industriel", 
                "GMAO & Maintenance", 
                "IoT & Edge Computing", 
                "Cybersécurité (OT/IT)", 
                "Transformation Digitale", 
                "Caisse & Encaissement", 
                "Général" 
            };

            var blogsByCategory = allCategories.ToDictionary(c => c, c => 0);

            var blogsByCategoryList = await _context.BlogPosts
                .GroupBy(b => b.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToListAsync();
                
            foreach (var item in blogsByCategoryList)
            {
                var catName = string.IsNullOrEmpty(item.Category) ? "Général" : item.Category;
                if (blogsByCategory.ContainsKey(catName))
                {
                    blogsByCategory[catName] += item.Count;
                }
                else
                {
                    blogsByCategory[catName] = item.Count;
                }
            }

            // Recent Activity Table (Last 5)
            var recentRequests = await _context.VisitorRequests
                .OrderByDescending(r => r.CreatedAt)
                .Take(5)
                .ToListAsync();

            var stats = new AdvancedDashboardStats
            {
                TotalUsers = totalUsers,
                TotalBlogs = totalBlogs,
                TotalRequests = totalRequests,
                PendingRequests = pendingRequests,
                RequestsTrend = requestsTrend,
                BlogsTrend = blogsTrend,
                RequestsByStatus = requestsByStatus,
                BlogsByCategory = blogsByCategory,
                RecentRequests = recentRequests
            };

            return Ok(stats);
        }
    }
}
