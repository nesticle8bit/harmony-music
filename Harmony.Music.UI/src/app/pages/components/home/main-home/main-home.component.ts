import { Component } from '@angular/core';
import { BrowseLibraryComponent } from "../elements/browse-library/browse-library.component";
import { RecentlyPlayedComponent } from "../elements/recently-played/recently-played.component";
import { RecentlyAddedComponent } from "../elements/recently-added/recently-added.component";

@Component({
  selector: 'app-main-home',
  standalone: true,
  imports: [BrowseLibraryComponent, RecentlyPlayedComponent, RecentlyAddedComponent],
  templateUrl: './main-home.component.html',
  styleUrl: './main-home.component.scss'
})
export class MainHomeComponent {

}
