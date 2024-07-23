import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClaudeApiService {
  private http = inject(HttpClient);
  private apiUrl = environment.azureFunctionUrl;

  getNegativeReply(prompt: string): Observable<ClaudeApiResponse> {
    return this.http.post<ClaudeApiResponse>(`${this.apiUrl}/api/GetNegativeReply`, { prompt });
  }
}

interface ClaudeApiResponse {
  response: string;
}
