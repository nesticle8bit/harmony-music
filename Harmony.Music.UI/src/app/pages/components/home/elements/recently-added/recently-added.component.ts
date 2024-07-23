import { Component, OnInit } from '@angular/core';
import { Playlist } from '../../../../../models/playlist.model';
import { IPlaylistService } from '../../../../../services/playlist.interface';
import { IPlayerService } from '../../../../../services/player.interface';

@Component({
  selector: 'app-recently-added',
  standalone: true,
  imports: [],
  templateUrl: './recently-added.component.html',
  styleUrl: './recently-added.component.scss',
})
export class RecentlyAddedComponent implements OnInit {
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
