import { Guid } from 'guid-typescript';

export class MovieClip {
  clipId: number; 
  moviePosterPath: string;
  moviePoster: File;
  videoClipPath: string;
  videoClip: File;
  uploadDate: Date;

  movieRef: Guid;
}
