import { Guid } from 'guid-typescript';

export class MovieClip {
  clipId: number; 
  moviePosterPath: string;
  movieTrailerUrl: string;
  videoClipPath: string;
  videoClipDuration: number;
  uploadDate: Date;

  movieRef: Guid;
}
