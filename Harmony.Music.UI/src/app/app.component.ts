import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LeftSidebarComponent } from './ui/left-sidebar/left-sidebar.component';
import { TopNavigatorComponent } from './ui/top-navigator/top-navigator.component';
import { RightSidebarComponent } from './ui/right-sidebar/right-sidebar.component';
import { StatusBarComponent } from './ui/status-bar/status-bar.component';
import { FooterComponent } from "./ui/footer/footer.component";
import { PlayerControlsComponent } from "./player/player-controls/player-controls.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    LeftSidebarComponent,
    TopNavigatorComponent,
    RightSidebarComponent,
    StatusBarComponent,
    FooterComponent,
    PlayerControlsComponent
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'Harmony Music 🎧️';
}
