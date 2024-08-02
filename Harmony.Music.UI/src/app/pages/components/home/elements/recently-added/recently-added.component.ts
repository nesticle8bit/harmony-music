import { Component, OnInit } from '@angular/core';
import { Playlist } from '../../../../../models/playlist.model';
import { IPlaylistService } from '../../../../../services/playlist.interface';
import { IPlayerService } from '../../../../../services/player.interface';
import { RouterModule } from '@angular/router';
import { environment } from '../../../../../../environments/environment';

@Component({
  selector: 'app-recently-added',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './recently-added.component.html',
  styleUrl: './recently-added.component.scss',
})
export class RecentlyAddedComponent implements OnInit {
  public imagePath: string = `${environment.apiUrl}/albums`;
  public recentlyAdded: Playlist[] = [];

  constructor(
    private playlistService: IPlaylistService,
    private playerService: IPlayerService
  ) {}

  ngOnInit(): void {
    this.getRecentlyAddedPlaylist();
  }

  getRecentlyAddedPlaylist(): void {
    this.playlistService
      .getRecentlyPlayedPlaylist()
      .subscribe((response: Playlist[]) => {
        this.recentlyAdded = response;
      });
  }

  playSong(id: string): void {
    this.playerService.playSong(+id);
  }
}
