import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IPlayerService } from './player.interface';

@Injectable({
  providedIn: 'root',
})
export class PlayerService implements IPlayerService {
  constructor(private http: HttpClient) {}

  playSong(id: number): Observable<Blob> {
    return this.http.get(`${environment.apiUrl}/api/player/${id}/play`, { responseType: 'blob' });
  }
}
