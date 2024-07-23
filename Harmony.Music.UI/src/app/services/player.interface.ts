import { Observable } from 'rxjs';

export abstract class IPlayerService {
  abstract currentTrack(): Observable<Blob>;
  abstract playSong(id: number): void;
}
