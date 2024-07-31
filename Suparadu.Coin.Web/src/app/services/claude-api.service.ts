import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClaudeApiService {
  private http = inject(HttpClient);
  private apiUrl = environment.azureFunctionUrl;

  getNegativeReply(prompt: string): Observable<string> {
    const headers = new HttpHeaders();
    headers.set('x-functions-key', environment.negativeFunctionKey);
    headers.set('Content-Type', 'text/plain');

    return this.http.post(`${this.apiUrl}/GetNegativeReply`, prompt, {
      headers: headers,
      responseType: 'text'
    });
  }
}
