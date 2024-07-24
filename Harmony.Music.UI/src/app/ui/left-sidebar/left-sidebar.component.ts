import { Component } from '@angular/core';
import { CurrentImageInfoTrackComponent } from '../../player/current-image-info-track/current-image-info-track.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-left-sidebar',
  standalone: true,
  imports: [CurrentImageInfoTrackComponent, RouterModule],
  templateUrl: './left-sidebar.component.html',
  styleUrl: './left-sidebar.component.scss',
})
export class LeftSidebarComponent {}
