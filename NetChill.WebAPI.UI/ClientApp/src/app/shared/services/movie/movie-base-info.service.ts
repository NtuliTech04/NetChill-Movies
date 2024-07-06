import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpErrorResponse} from '@angular/common/http';

import { environment } from 'src/environments/environment.development';
import { MovieBaseInfo } from 'src/app/core/models/movie/movie-base-info.model';

@Injectable({
  providedIn: 'root'
})
export class MovieBaseInfoService {

  private URL: string = environment.apiBaseUrl;
  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private http: HttpClient) { }


  //Creates movie base info
  addBaseInfo(data: MovieBaseInfo): Observable<any> {
    let url = `${this.URL}/Movies/info`;
    return this.http.post(url, data).pipe(catchError(this.errorHandler));
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
