import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { MovieBaseInfo } from 'src/app/core/models/movie/movie-base-info.model';
import { MovieClip } from 'src/app/core/models/movie/movie-clip.model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MovieStreamingService {
  private URL: string = environment.apiBaseUrl;
  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private http: HttpClient) { }

  //#region All Movies Lists

  //Get all movies info
  getBaseInfoList(): Observable<MovieBaseInfo[]> {
    let url = `${this.URL}/Movies/info/list`;
    return this.http.get<MovieBaseInfo[]>(url);
  }

  //Get all movies files
  getAllMediaFiles(): Observable<MovieClip[]> {
    let url = `${this.URL}/Movies/mediafiles/list`;
    return this.http.get<MovieClip[]>(url);
  }

  //#endregion All Movies List End


  //#region  Organized Movie Lists

  //#region Upcoming Movies
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
  //#endregion

  //#region Latest (New Movies)
  //Get all latest movies info
  getLatestInfoList(): Observable<MovieBaseInfo[]> {
    let url = `${this.URL}/Movies/latest-info/list`;
    return this.http.get<MovieBaseInfo[]>(url);
  }

  //Get all latest movies files
  getLatestMediaList(): Observable<MovieClip[]> {
    let url = `${this.URL}/Movies/latest-media/list`;
    return this.http.get<MovieClip[]>(url);
  }
  //#endregion
  
  //#region  Featured Movies
  //Get all featured movies info
  getFeaturedInfoList(): Observable<MovieBaseInfo[]> {
    let url = `${this.URL}/Movies/featured-info/list`;
    return this.http.get<MovieBaseInfo[]>(url);
  }

  //Get all featured movies files
  getFeaturedMediaList(): Observable<MovieClip[]> {
    let url = `${this.URL}/Movies/featured-media/list`;
    return this.http.get<MovieClip[]>(url);
  }
  //#endregion

  //#endregion


  //#region Get Movie Data By Id/Ref

  //Get Movie Info 
  readMovieInfo(id: Guid): Observable<any> {
    let url = `${this.URL}/Movies/read-info/${id}`;
    return this.http.get(url, { headers: this.headers }).pipe(
      map((res: Response) => {
        return res || {};
      }),
      catchError(this.errorHandler)
    );
  }

  //Get Movie Production 
  readMovieProduction(id: Guid): Observable<any> {
    let url = `${this.URL}/Movies/read-production/${id}`;
    return this.http.get(url, { headers: this.headers }).pipe(
      map((res: Response) => {
        return res || {};
      }),
      catchError(this.errorHandler)
    );
  }

  //Get Movie Files 
  readMovieFiles(id: Guid): Observable<any> {
    let url = `${this.URL}/Movies/read-files/${id}`;
    return this.http.get(url, { headers: this.headers }).pipe(
      map((res: Response) => {
        return res || {};
      }),
      catchError(this.errorHandler)
    );
  }

  //#endregion


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
