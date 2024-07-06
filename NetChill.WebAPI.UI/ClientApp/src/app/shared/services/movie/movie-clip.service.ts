import { Guid } from 'guid-typescript';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MovieClipService {
  private URL: string = environment.apiBaseUrl;
  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private http: HttpClient) { }


  //Upload Movie Files
  uploadMovieFiles(ref: Guid, data: FormData): Observable<any> {
    let url = `${this.URL}/Movies/mediafiles/${ref}`;
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
      //Get server-side error
      errorMessage = `Error Code: ${ex.status}\nMessage: ${ex.message}`;
    }
    console.log(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }
}
