import { Component } from '@angular/core';
import { IPlayerService } from '../../../../services/player.interface';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-artist-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './artist-page.component.html',
  styleUrl: './artist-page.component.scss',
})
export class ArtistPageComponent {
  public artistHash: string = '';
  public artist: any;
  public topSongs: any[] = [];

  constructor(
    private playerService: IPlayerService,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.paramMap.subscribe((paramMap: any) => {
      this.artistHash = paramMap.params.hash;
      this.getArtistInfo();
      this.getTopSongs();
    });
  }

  getArtistInfo(): void {
    if (!this.artistHash) {
      return;
    }

    this.playerService
      .getArtistInfo(this.artistHash)
      .subscribe((response: any) => {
        this.artist = response;
      });
  }

  getTopSongs(): void {
    if (!this.artistHash) {
      return;
    }
    
    this.playerService
      .getTopSongs(this.artistHash)
      .subscribe((response: any) => {
        this.topSongs = response;
      });
  }
}
