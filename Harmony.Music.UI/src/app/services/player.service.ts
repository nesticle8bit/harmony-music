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
    currentTrack: new Subject<Blob>(),
  };

  constructor(private http: HttpClient) {}

  currentTrack(): Observable<Blob> {
    return this.subjects.currentTrack.asObservable();
  }

  playSong(id: number): void {
    this.http
      .get(`${environment.apiUrl}/api/player/${id}/play`, {
        responseType: 'blob',
      })
      .subscribe((blob: Blob) => {
        this.subjects.currentTrack.next(blob);
      });
  }
}
