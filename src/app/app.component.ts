import { Component, OnInit, HostListener } from '@angular/core';
import { Router, RouterOutlet, Event, NavigationStart, NavigationEnd, NavigationCancel, NavigationError } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { LoadingScreenComponent } from './components/loading-screen/loading-screen.component';
import { LoadingService } from './services/loading.service';
import { gsap } from 'gsap';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeaderComponent, FooterComponent, LoadingScreenComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'simsoft-app';

  private cursorHalo: HTMLElement | null = null;
  private xToHalo: any;
  private yToHalo: any;

  constructor(private router: Router, public loadingService: LoadingService) {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationStart) {
        this.loadingService.show();
      }
      if (event instanceof NavigationEnd || event instanceof NavigationCancel || event instanceof NavigationError) {
        setTimeout(() => {
          this.loadingService.hide();
        }, 800);
      }
    });
  }

  ngOnInit() {
    setTimeout(() => {
      this.loadingService.hide();
    }, 1500);

    if (typeof document !== 'undefined') {
      this.cursorHalo = document.getElementById('cursor-halo');

      if (this.cursorHalo) {
        gsap.set(this.cursorHalo, { xPercent: -50, yPercent: -50, x: -200, y: -200 });
        this.xToHalo = gsap.quickTo(this.cursorHalo, 'x', { duration: 0.15, ease: 'power2' });
        this.yToHalo = gsap.quickTo(this.cursorHalo, 'y', { duration: 0.15, ease: 'power2' });
      }
    }
  }

  @HostListener('document:mousemove', ['$event'])
  onMouseMove(event: MouseEvent) {
    if (this.xToHalo && this.yToHalo) {
      this.xToHalo(event.clientX);
      this.yToHalo(event.clientY);
    }
  }

  @HostListener('document:mouseover', ['$event'])
  onMouseOver(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (this.cursorHalo) {
      if (target.closest('a') || target.closest('button') || target.closest('.cursor-pointer')) {
        this.cursorHalo.classList.add('cursor-hover-state');
      } else {
        this.cursorHalo.classList.remove('cursor-hover-state');
      }
    }
  }
}
