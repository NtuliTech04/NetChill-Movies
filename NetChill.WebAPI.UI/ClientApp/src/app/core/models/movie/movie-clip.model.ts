import { Guid } from 'guid-typescript';

export class MovieClip {
  clipId: number; 
  moviePosterPath: string;
  movieTrailerUrl: string;
  videoClipPath: string;
  uploadDate: Date;

  movieRef: Guid;
}
