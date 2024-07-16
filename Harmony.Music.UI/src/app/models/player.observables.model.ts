import { Observable } from 'rxjs';

export class PlayerObservables {
  currentTrack: Observable<Observable<Blob | undefined>> | undefined;
}
