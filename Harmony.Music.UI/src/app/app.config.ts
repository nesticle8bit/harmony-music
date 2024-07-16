import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { IPlaylistService } from './services/playlist.interface';
import { PlaylistService } from './services/playlist.service';
import { provideHttpClient } from '@angular/common/http';
import { IPlayerService } from './services/player.interface';
import { PlayerService } from './services/player.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(),
    provideAnimationsAsync(),
    provideAnimationsAsync(),
    provideHttpClient(),

    { provide: IPlaylistService, useClass: PlaylistService },
    { provide: IPlayerService, useClass: PlayerService },
  ],
};
