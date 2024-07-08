import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CoinInfoService {
  private mockMarketCap: number = 500000; // Starting mock value

  constructor() {}

  getMarketCap(): Observable<number> {
    // Simulating market cap increase
    this.mockMarketCap += Math.random() * 100000;
    return of(this.mockMarketCap);
  }
}