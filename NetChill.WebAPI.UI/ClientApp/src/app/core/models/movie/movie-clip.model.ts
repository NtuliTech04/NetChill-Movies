import { Guid } from 'guid-typescript';

export class MovieClip {
  clipId: number; 
  moviePosterPath: string;
  videoClipPath: string;
  movieTrailerUrl: string;
  uploadDate: Date;

  movieRef: Guid;
}
