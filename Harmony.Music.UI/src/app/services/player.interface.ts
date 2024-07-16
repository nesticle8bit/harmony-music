import { PlayerObservables } from '../models/player.observables.model';
import { Observable } from 'rxjs';

export abstract class IPlayerService {
  abstract currentTrack(): Observable<Observable<Blob> | undefined>;
  abstract playSong(id: number): void;
}
