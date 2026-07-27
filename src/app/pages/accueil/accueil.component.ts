import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GsapAnimateDirective } from '../../directives/gsap-animate.directive';

interface SolutionItem {
  title: string;
  desc: string;
  image: string;
  sysId: string;
  badges: string[];
}

@Component({
  selector: 'app-accueil',
  standalone: true,
  imports: [CommonModule, RouterModule, GsapAnimateDirective],
  templateUrl: './accueil.component.html',
  styleUrls: ['./accueil.component.css']
})
export class AccueilComponent {
  activeSolutionKey: string = 'divalto';

  solutionsData: Record<string, SolutionItem> = {
    divalto: {
      title: "Divalto ERP",
      desc: "Pilotez l'intégralité de votre PME avec puissance.",
      image: "https://lh3.googleusercontent.com/aida-public/AB6AXuDbcUtcaVNU4QslU0Ls91a_0REjp6lqpTmnFWZWx66NPMOS7TcajOrHqq94Pv_huWNdXGL4KnkkCe-xEWDlXP1CvIYqO9QAGnQxUrNFU7Ya0jLtxjOcqccpvq2IdsqBW6VPvK9trdl6NGls9LQdH58ozA9Vtuf7JAKUknsL1p2gytxoY3XYz_B1Atd_Egx9egvruyyBw1Sny4jONuPMb4022sQ9AMd-nCgxNJsQvrYd_aaXUnceIxD-",
      sysId: "ERP_DIV_01",
      badges: ["Intégration totale", "Scalabilité", "BI Native"]
    },
    wavesoft: {
      title: "WaveSoft ERP",
      desc: "La gestion agile pour les entreprises en croissance.",
      image: "https://lh3.googleusercontent.com/aida-public/AB6AXuDbcUtcaVNU4QslU0Ls91a_0REjp6lqpTmnFWZWx66NPMOS7TcajOrHqq94Pv_huWNdXGL4KnkkCe-xEWDlXP1CvIYqO9QAGnQxUrNFU7Ya0jLtxjOcqccpvq2IdsqBW6VPvK9trdl6NGls9LQdH58ozA9Vtuf7JAKUknsL1p2gytxoY3XYz_B1Atd_Egx9egvruyyBw1Sny4jONuPMb4022sQ9AMd-nCgxNJsQvrYd_aaXUnceIxD-",
      sysId: "ERP_WAV_02",
      badges: ["Simple", "Robuste", "Personnalisable"]
    },
    firstparc: {
      title: "First Parc (GMAO)",
      desc: "Optimisez la maintenance de vos actifs critiques.",
      image: "https://lh3.googleusercontent.com/aida-public/AB6AXuDbcUtcaVNU4QslU0Ls91a_0REjp6lqpTmnFWZWx66NPMOS7TcajOrHqq94Pv_huWNdXGL4KnkkCe-xEWDlXP1CvIYqO9QAGnQxUrNFU7Ya0jLtxjOcqccpvq2IdsqBW6VPvK9trdl6NGls9LQdH58ozA9Vtuf7JAKUknsL1p2gytxoY3XYz_B1Atd_Egx9egvruyyBw1Sny4jONuPMb4022sQ9AMd-nCgxNJsQvrYd_aaXUnceIxD-",
      sysId: "GMAO_FST_03",
      badges: ["Préventif", "QR Codes", "Reporting"]
    },
    dev: {
      title: "Développement spécifique",
      desc: "Des solutions sur mesure pour vos besoins uniques.",
      image: "https://lh3.googleusercontent.com/aida-public/AB6AXuBvZjhgpRyM4-eGub9jd2IQnq0uK4okxr_oxjIHPnZLBdH17t03H4no0dJ3zNG2RMycuVrW3ObbworSq-xtFt2DRiu4SeGyfftjRuv0GarGBvfFPck3ymclLvxQFlvU8Qapa3jlgsoCAFVeF4ae50bFFy9zED46hm18N_GMUXDfr9aOfmzL4CyYSAH5lYg8i5Hy-up2RaAvmZLUpqbCB9q7yzzDflr77_u_ZGgq_K8tmE3uhwFHWsMe",
      sysId: "DEV_CUS_04",
      badges: ["API First", "Cloud Native", "DevOps"]
    },
    quorion: {
      title: "Solutions d'encaissement (QUORiON)",
      desc: "Simplifiez la gestion de vos points de vente.",
      image: "https://lh3.googleusercontent.com/aida-public/AB6AXuCo_8iTt_cocprsqftJpRJiuua32uCGyZHZeuizs8IQxyfraf3qsKEmxsTByiZbV3ujRmUVnLGEVu-m_Q-MbFvRKzDzA5yRAp7-CRNNM5o6a8s68S8k4qNaNFqh-2IQSLXe7yHGD_t25Slo8ib5vqvVh_9Bbi8fgKK8rY69jJI63ZChzY3PXm88BAAcn-TthA4PIwvf3AmC47vGxgXWQ43Cpre0yr9TatknnvKO-53UuEZRUFmrw2UY",
      sysId: "POS_QUO_05",
      badges: ["Fiabilité", "Tactile", "Stock temps réel"]
    },
    balances: {
      title: "Balances électroniques",
      desc: "Précision et conformité pour votre pesage.",
      image: "https://lh3.googleusercontent.com/aida-public/AB6AXuCo_8iTt_cocprsqftJpRJiuua32uCGyZHZeuizs8IQxyfraf3qsKEmxsTByiZbV3ujRmUVnLGEVu-m_Q-MbFvRKzDzA5yRAp7-CRNNM5o6a8s68S8k4qNaNFqh-2IQSLXe7yHGD_t25Slo8ib5vqvVh_9Bbi8fgKK8rY69jJI63ZChzY3PXm88BAAcn-TthA4PIwvf3AmC47vGxgXWQ43Cpre0yr9TatknnvKO-53UuEZRUFmrw2UY",
      sysId: "SCL_MET_06",
      badges: ["Métrologie légale", "Connecté", "Haute précision"]
    },
    support: {
      title: "Assistance & Support",
      desc: "Une expertise technique à vos côtés 24/7.",
      image: "https://lh3.googleusercontent.com/aida-public/AB6AXuC8FQfjh7njWm6ffRky8xO6Uk_Cq-9GrBPopNrXLd82TWZ1TBfm3y6xbEsMFNTnvaig68UPlQgf6Z_mLvwJl1oNKC4wzLbo-nigWXivORSZFDLxUk2ur6O5SuldsQRGdv1T7lkWU-BNa9guDfBI2aovdLsrKRkLupN9XrtQzkv6uZ79CvmTjNKy3wE-IwklZMOYi-16uNFXKRudv0xM2YSze8_-NcoecG6btLqEbGxBZ3BZqWt2xZi7",
      sysId: "SUP_HLP_07",
      badges: ["Réactivité", "Sécurité", "Accompagnement"]
    }
  };

  get activeSolution(): SolutionItem {
    return this.solutionsData[this.activeSolutionKey] || this.solutionsData['divalto'];
  }

  selectSolution(key: string): void {
    this.activeSolutionKey = key;
  }

  openFaqIndex: number | null = null;

  toggleFaq(index: number): void {
    this.openFaqIndex = this.openFaqIndex === index ? null : index;
  }
}
