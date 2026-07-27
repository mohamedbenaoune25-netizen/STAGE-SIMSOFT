import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface ReferenceItem {
  id: number;
  name: string;
  category: string;
  logo: string;
  description: string;
  badge: string;
  stats: string;
}

@Component({
  selector: 'app-references',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './references.component.html',
  styleUrls: ['./references.component.css']
})
export class ReferencesComponent {
  activeCategory: string = 'Tous';
  selectedReference: ReferenceItem | null = null;

  categories: string[] = ['Tous', 'Industrie', 'Retail & POS', 'Logistique', 'Finance & Public', 'IoT & Cloud'];

  references: ReferenceItem[] = [
    {
      id: 1,
      name: 'CNP Assurances',
      category: 'Finance & Public',
      logo: 'images/références/CNP.png',
      description: 'Modernisation des systèmes d\'information et gestion sécurisée des flux financiers et assurantiels.',
      badge: 'Finance',
      stats: '100% Sécurisé'
    },
    {
      id: 2,
      name: 'Falcon Aviation',
      category: 'Logistique',
      logo: 'images/références/Falcon-logo.png',
      description: 'Gestion avancée de flotte critique, suivi prédictif et télémesure haute précision.',
      badge: 'Aéronautique',
      stats: '0.2s Réponse'
    },
    {
      id: 3,
      name: 'GLC Industries',
      category: 'Industrie',
      logo: 'images/références/GLC.png',
      description: 'Déploiement ERP industriel complet Divalto et automatisation des lignes de production.',
      badge: 'ERP Industriel',
      stats: '99.9% Uptime'
    },
    {
      id: 4,
      name: 'Gravic Group',
      category: 'IoT & Cloud',
      logo: 'images/références/Gravic.png',
      description: 'Traçabilité industrielle globale, contrôle qualité automatisé et capteurs IoT.',
      badge: 'IoT & Traçabilité',
      stats: '5M+ Données/j'
    },
    {
      id: 5,
      name: 'CGPR',
      category: 'Finance & Public',
      logo: 'images/références/LOGO-CGPR.png',
      description: 'Architecture de réseau distribué haute sécurité et gouvernance numérique.',
      badge: 'Secteur Public',
      stats: 'ISO 27001'
    },
    {
      id: 6,
      name: 'Sotufab',
      category: 'Retail & POS',
      logo: 'images/références/Sotufab-240x130.png',
      description: 'Synchronisation omnicanale des magasins et gestion des stocks temps réel.',
      badge: 'Retail ERP',
      stats: '50+ Magasins'
    },
    {
      id: 7,
      name: 'Glass Engineering',
      category: 'Industrie',
      logo: 'images/références/glass-removebg-preview.png',
      description: 'Systèmes de pesage haute précision intégrés et gestion de métrologie.',
      badge: 'Ingénierie',
      stats: 'Precision 0.01g'
    },
    {
      id: 8,
      name: 'Image Systems',
      category: 'IoT & Cloud',
      logo: 'images/références/images-removebg-preview.png',
      description: 'Infrastructure cloud hybride et protection des données d\'entreprise.',
      badge: 'Cloud Native',
      stats: '24/7 Monitoring'
    },
    {
      id: 9,
      name: 'Sotufab Plast',
      category: 'Industrie',
      logo: 'images/références/logo-sotufab-plast-tunisie-1.png',
      description: 'GMAO First Parc pour la gestion automatisée de la maintenance des équipements.',
      badge: 'GMAO Active',
      stats: '-35% Pannes'
    },
    {
      id: 10,
      name: 'Spolo Distribution',
      category: 'Retail & POS',
      logo: 'images/références/spolo.png',
      description: 'Solutions d\'encaissement intelligentes QUORiON et gestion de caisse avancée.',
      badge: 'POS QUORiON',
      stats: '100k Trans/j'
    },
    {
      id: 11,
      name: 'Watts Energy',
      category: 'IoT & Cloud',
      logo: 'images/références/watts.png',
      description: 'Monitoring télémetrique des ressources et dashboards analytiques.',
      badge: 'Télémesure',
      stats: 'Realtime Data'
    },
    {
      id: 12,
      name: 'DarkBlue Tech',
      category: 'Finance & Public',
      logo: 'images/références/darkblue.webp',
      description: 'Audit de sécurité, architecture Zero-Trust et protection cybernétique.',
      badge: 'Cybersécurité',
      stats: 'Zero-Trust'
    }
  ];

  get filteredReferences(): ReferenceItem[] {
    if (this.activeCategory === 'Tous') {
      return this.references;
    }
    return this.references.filter(item => item.category === this.activeCategory);
  }

  setCategory(cat: string) {
    this.activeCategory = cat;
  }

  openModal(item: ReferenceItem) {
    this.selectedReference = item;
  }

  closeModal() {
    this.selectedReference = null;
  }
}
