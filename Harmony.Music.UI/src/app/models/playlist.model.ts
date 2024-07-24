import { Artist } from './artist.model';

export class Playlist {
  id!: string;
  name!: string;
  image!: string;
  artist!: Artist;
  dateCreated!: Date;
}
