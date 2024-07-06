import { Guid } from 'guid-typescript';

export class MovieProduction {
  productionId: number;
  directors: string;
  writers: string;
  movieStars: string;

  movieRef: Guid;
}
