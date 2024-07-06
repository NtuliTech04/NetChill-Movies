import { Guid } from 'guid-typescript';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { MovieProduction } from 'src/app/core/models/movie/movie-production.model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MovieProductionService {
  private URL: string = environment.apiBaseUrl;
  headers = new HttpHeaders().set('Content-Type', 'application/json');


  constructor(private http: HttpClient) { }

  //Create Movie Production
  creatMovieProduction(ref: Guid, data: MovieProduction): Observable<any> {
    let url = `${this.URL}/Movies/production/${ref}`;
    return this.http.post(url, data, { headers: this.headers })
    .pipe(catchError(this.errorHandler));
  }


  //Error handling
  errorHandler(ex: HttpErrorResponse) {
    let errorMessage = '';
    if (ex.error instanceof ErrorEvent) {
      //Get client-side error
      errorMessage = ex.error.message;
    }
    else{
      //Get server-side error
      errorMessage = `Error Code: ${ex.status}\nMessage: ${ex.message}`;
    }
    console.log(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }
}
