import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { IPlaylistService } from './services/playlist.interface';
import { PlaylistService } from './services/playlist.service';
import { IPlayerService } from './services/player.interface';
import { PlayerService } from './services/player.service';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),

    { provide: IPlaylistService, useClass: PlaylistService },
    { provide: IPlayerService, useClass: PlayerService },
  ],
};
