import { Observable } from 'rxjs';
import { Playlist } from '../models/playlist.model';

export abstract class IPlaylistService {
  abstract getRecentlyPlayedPlaylist(): Observable<Playlist[]>;
}
