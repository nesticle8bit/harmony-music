import { Component, OnInit } from '@angular/core';
import { IPlayerService } from '../../services/player.interface';
import { Observable } from 'rxjs';

declare var Audio: any;

@Component({
  selector: 'app-player-controls',
  standalone: true,
  imports: [],
  templateUrl: './player-controls.component.html',
  styleUrl: './player-controls.component.scss',
})
export class PlayerControlsComponent implements OnInit {
  private audio: HTMLAudioElement | undefined = undefined;

  constructor(private playerService: IPlayerService) {
    // this.audio = new Audio();
  }

  ngOnInit(): void {
    this.getCurrentTrack();
  }

  getCurrentTrack(): void {
    this.playerService
      .currentTrack()
      .subscribe((currentTrack: Observable<Blob> | undefined) => {
        if (!currentTrack) {
          return;
        }

        currentTrack.subscribe((blob: Blob) => {
          const audioUrl = URL.createObjectURL(blob);

          // Pause any currently playing audio
          if (this.audio?.paused === false) {
            this.audio.pause();
            this.audio.currentTime = 0; // Reset to beginning
          }

          if (!this.audio) {
            return;
          }

          // Set new audio source and play
          this.audio.src = audioUrl;
          this.audio?.play();
        });
      });
  }

  // stopSong(): void {
  //   if (this.audio) {
  //     this.audio.pause();
  //     this.audio.currentTime = 0; // Reset to beginning
  //   }
  // }
}
