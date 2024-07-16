import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { IPlaylistService } from './playlist.interface';
import { Playlist } from '../models/playlist.model';
import { environment } from '../../environments/environment';
import { APIResponse } from '../models/response.model';

@Injectable({
  providedIn: 'root',
})
export class PlaylistService implements IPlaylistService {
  constructor(private http: HttpClient) {}

  getRecentlyPlayedPlaylist(): Observable<Playlist[]> {
    return this.http
      .get<any>(`${environment.apiUrl}/api/playlists/recently-added`)
      .pipe(
        map((response: APIResponse) => response.data),
        catchError((error) => {
          console.error('Error fetching users:', error);
          return throwError(error);
        })
      );
  }
}
