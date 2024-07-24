import { Component } from '@angular/core';
import { IPlayerService } from '../../../../services/player.interface';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-artist-page',
  standalone: true,
  imports: [],
  templateUrl: './artist-page.component.html',
  styleUrl: './artist-page.component.scss',
})
export class ArtistPageComponent {
  public artistHash: string = '';

  constructor(
    private playerService: IPlayerService,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.paramMap.subscribe((paramMap: any) => {
      this.artistHash = paramMap.params.hash;
      this.getArtistInfo();
    });
  }

  getArtistInfo(): void {
    if (!this.artistHash) {
      return;
    }

    this.playerService
      .getArtistInfo(this.artistHash)
      .subscribe((response: any) => {
        console.log(response);
      });
  }
}
