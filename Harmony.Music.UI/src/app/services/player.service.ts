import { environment } from '../../environments/environment';
import { IPlayerService } from './player.interface';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PlayerService implements IPlayerService {
  private subjects = {
    currentTrack: new BehaviorSubject<Observable<Blob> | undefined>(undefined),
  };

  constructor(private http: HttpClient) {}

  currentTrack(): Observable<Observable<Blob> | undefined> {
    return this.subjects.currentTrack.asObservable();
  }

  playSong(id: number): void {
    this.subjects.currentTrack.next(this.http.get(`${environment.apiUrl}/api/player/${id}/play`, { responseType: 'blob' }));
  }
}
