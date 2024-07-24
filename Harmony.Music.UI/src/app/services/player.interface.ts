import { Observable } from 'rxjs';

export abstract class IPlayerService {
  abstract currentTrackBlob(): Observable<Blob>;
  abstract currentTrackInfo(): Observable<any>;
  abstract playSong(id: number): void;
  abstract getArtistInfo(artistHash: string): Observable<any>;
}
