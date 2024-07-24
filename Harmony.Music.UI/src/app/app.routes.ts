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
    path: 'settings',
    component: SettingsComponent,
    canActivate: [],
  },
];
