import { Observable } from 'rxjs';
import { Playlist } from '../models/playlist.model';

export abstract class IPlayerService {
  abstract playSong(id: number): Observable<Blob>;
}
