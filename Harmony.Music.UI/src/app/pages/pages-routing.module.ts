import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainHomeComponent } from './components/home/main-home/main-home.component';
import { SettingsComponent } from './components/settings/settings.component';
import { ArtistPageComponent } from './components/pages/artist-page/artist-page.component';
import { SearchComponent } from './components/pages/search/search.component';

const routes: Routes = [
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
  {
    path: 'artist/:hash',
    component: ArtistPageComponent,
    canActivate: [],
  },
  {
    path: 'search',
    component: SearchComponent,
    canActivate: [],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
