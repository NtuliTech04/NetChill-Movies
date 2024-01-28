import { Guid } from "guid-typescript";

export class MovieBaseInfo {
  movieId: Guid;
  title: string;
  genre: string;
  description: string;
  languages: string;
  isFeatured: boolean;
  isUpcoming: boolean;
  yearReleased: number;
  availableFrom: Date;
  avgRating: number;
}
