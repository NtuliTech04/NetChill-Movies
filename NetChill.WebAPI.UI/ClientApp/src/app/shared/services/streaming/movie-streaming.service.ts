import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { MovieBaseInfo } from 'src/app/core/models/movie/movie-base-info.model';
import { MovieClip } from 'src/app/core/models/movie/movie-clip.model';
import { MovieProduction } from 'src/app/core/models/movie/movie-production.model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MovieStreamingService {
  private URL: string = environment.apiBaseUrl;
  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private http: HttpClient) { }

  //Get all upcoming movies info
  getUpcomingInfoList(): Observable<MovieBaseInfo[]> {
    let url = `${this.URL}/Movies/upcoming-info/list`;
    return this.http.get<MovieBaseInfo[]>(url);
  }


  //Get all upcoming movies files
  getUpcomingMediaList(): Observable<MovieClip[]> {
    let url = `${this.URL}/Movies/upcoming-media/list`;
    return this.http.get<MovieClip[]>(url);
  }
  




  //Get all movies info
  getBaseInfoList(): Observable<MovieBaseInfo[]> {
    let url = `${this.URL}/Movies/info/list`;
    return this.http.get<MovieBaseInfo[]>(url);
  }

  //Get all movies production
  getProductionList(): Observable<MovieProduction[]> {
    let url = `${this.URL}/Movies/production/list`;
    return this.http.get<MovieProduction[]>(url);
  }

  //Get all movies files
  getAllMediaFiles(): Observable<MovieClip[]> {
    let url = `${this.URL}/Movies/mediafiles/list`;
    return this.http.get<MovieClip[]>(url);
  }







//Error handling
errorHandler(ex: HttpErrorResponse) {
  let errorMessage = '';
  if (ex.error instanceof ErrorEvent) {
    //Get client-side error
    errorMessage = ex.error.message;
  }
  else{
    //Get server-side erro
    errorMessage = `Error Code: ${ex.status}\nMessage: ${ex.message}`;
  }
  console.log(errorMessage);
  return throwError(() => {
    return errorMessage;
  });
}

}
