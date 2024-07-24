import { Routes } from '@angular/router';
import { MainHomeComponent } from './pages/components/home/main-home/main-home.component';
import { SettingsComponent } from './pages/components/settings/settings.component';

export const routes: Routes = [
  {
    path: '',
    component: MainHomeComponent,
    canActivate: [],
  },

  {
    path: "pages",
    loadChildren: () =>
      import('./pages/pages.module').then((m) => m.PagesModule),
  },
];
