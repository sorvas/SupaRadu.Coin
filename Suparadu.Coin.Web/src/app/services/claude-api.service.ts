import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import { environment } from '../../environments/environment';
import {catchError} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class ClaudeApiService {
  private http = inject(HttpClient);
  private apiUrl = environment.azureFunctionUrl;

  getNegativeReply(prompt: string): Observable<string> {
    const headers = new HttpHeaders()
      .set('x-functions-key', environment.negativeFunctionKey)
      .set('Content-Type', 'text/plain');

    return this.http.post(`${this.apiUrl}/api/GetNegativeReply`, prompt, {
      headers: headers,
      responseType: 'text'
    }).pipe(
      catchError(error => {
        console.error('Error calling Claude API:', error);
        return throwError(() => new Error('Something went wrong; please try again later.'));
      })
    );
  }
}
