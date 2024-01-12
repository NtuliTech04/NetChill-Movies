import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { MovieLanguage } from 'src/app/core/models/movie/movie-language.model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MovieLanguageService {
  private URL: string = environment.apiBaseUrl;
  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private http: HttpClient) { }


  //Gets all moviegenres
  getAllLanguages(): Observable<MovieLanguage[]> {
    let url = `${this.URL}/Languages/list`;
    return this.http.get<MovieLanguage[]>(url);
  }


  //Create Movie Genre
  createLanguage(data: MovieLanguage): Observable<any> {
    let url = `${this.URL}/Languages/create`;
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
