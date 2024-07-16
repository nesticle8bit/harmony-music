import { Component } from '@angular/core';
import { CurrentImageInfoTrackComponent } from "../../player/current-image-info-track/current-image-info-track.component";

@Component({
  selector: 'app-left-sidebar',
  standalone: true,
  imports: [CurrentImageInfoTrackComponent],
  templateUrl: './left-sidebar.component.html',
  styleUrl: './left-sidebar.component.scss'
})
export class LeftSidebarComponent {

}
