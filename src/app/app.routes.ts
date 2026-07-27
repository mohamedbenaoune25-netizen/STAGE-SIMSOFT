import { Routes } from '@angular/router';
import { AccueilComponent } from './pages/accueil/accueil.component';
import { AProposComponent } from './pages/a-propos/a-propos.component';
import { BlogComponent } from './pages/blog/blog.component';
import { ProduitsComponent } from './pages/produits/produits.component';
import { ReferencesComponent } from './pages/references/references.component';
import { ServicesComponent } from './pages/services/services.component';

export const routes: Routes = [
  { path: '', redirectTo: 'accueil', pathMatch: 'full' },
  { path: 'accueil', component: AccueilComponent },
  { path: 'a-propos', component: AProposComponent },
  { path: 'blog', component: BlogComponent },
  { path: 'produits', component: ProduitsComponent },
  { path: 'references', component: ReferencesComponent },
  { path: 'services', component: ServicesComponent },
  { path: '**', redirectTo: 'accueil' }
];
