import { CommonModule, isPlatformBrowser } from '@angular/common';
import { IPlayerService } from '../../services/player.interface';
import {
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
  PLATFORM_ID,
} from '@angular/core';
import { FormsModule } from '@angular/forms';

declare var Audio: any;

@Component({
  selector: 'app-player-controls',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './player-controls.component.html',
  styleUrl: './player-controls.component.scss',
})
export class PlayerControlsComponent implements OnInit {
  private audio: HTMLAudioElement | undefined;
  public isBrowser: boolean;
  public currentTime: number = 0;
  public duration: number = 0;
  public isPlaying: boolean = false;

  constructor(
    private playerService: IPlayerService,
    @Inject(PLATFORM_ID) private platformId: object,
    private cdRef: ChangeDetectorRef
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);

    if (this.isBrowser) {
      this.audio = new Audio();
    }
  }

  ngOnInit(): void {
    this.getCurrentTrack();
  }

  getCurrentTrack(): void {
    this.playerService.currentTrack().subscribe((currentTrack: Blob) => {
      if (!currentTrack) {
        return;
      }

      const audioUrl = URL.createObjectURL(currentTrack);

      if (this.audio?.paused === false) {
        this.audio.pause();
        this.audio.currentTime = 0;
      }

      if (!this.audio) {
        return;
      }

      this.audio.src = audioUrl;
      this.audio?.play();

      this.audio!.addEventListener('timeupdate', () => {
        debugger;
        this.currentTime = this.audio!.currentTime;
        this.cdRef.detectChanges();
      });

      this.audio!.addEventListener('durationchange', () => {
        this.duration = this.audio!.duration;
        this.cdRef.detectChanges();
      });
    });
  }

  pause(): void {
    if (!this.audio) {
      return;
    }

    if (this.audio.paused) {
      this.audio.play();
    } else {
      this.audio.pause();
    }
  }

  volume(control: any): void {
    if (!this.audio) {
      return;
    }

    this.audio.volume = control.value / 100;
  }

  // stopSong(): void {
  //   if (this.audio) {
  //     this.audio.pause();
  //     this.audio.currentTime = 0; // Reset to beginning
  //   }
  // }

  seek(target: any): void {
    if (this.audio) {
      this.audio.currentTime = target.value;
    }
  }
}
