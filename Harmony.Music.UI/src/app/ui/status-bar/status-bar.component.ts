import { Component } from '@angular/core';
import { CurrentTrackComponent } from "../../player/current-track/current-track.component";
import { PlayerControlsComponent } from "../../player/player-controls/player-controls.component";

@Component({
  selector: 'app-status-bar',
  standalone: true,
  imports: [CurrentTrackComponent, PlayerControlsComponent],
  templateUrl: './status-bar.component.html',
  styleUrl: './status-bar.component.scss'
})
export class StatusBarComponent {

}
