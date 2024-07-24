import { environment } from '../../environments/environment';
import { IPlayerService } from './player.interface';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PlayerService implements IPlayerService {
  private subjects = {
    currentTrackInfo: new Subject<any>(),
    currentTrackBlob: new Subject<Blob>(),
  };

  constructor(private http: HttpClient) {}

  currentTrackBlob(): Observable<Blob> {
    return this.subjects.currentTrackBlob.asObservable();
  }

  currentTrackInfo(): Observable<any> {
    return this.subjects.currentTrackInfo.asObservable();
  }

  playSong(id: number): void {
    this.http
      .get(`${environment.apiUrl}/api/player/${id}/info`)
      .subscribe((songInfo: any) => {
        // TODO: create songInfo model
        this.subjects.currentTrackInfo.next(songInfo);

        this.http
          .get(`${environment.apiUrl}/api/player/${id}/play`, {
            responseType: 'blob',
          })
          .subscribe((blob: Blob) => {
            this.subjects.currentTrackBlob.next(blob);
          });
      });
  }
}
