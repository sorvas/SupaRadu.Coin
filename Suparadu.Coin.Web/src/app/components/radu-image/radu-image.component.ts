import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { CoinInfoService } from '../../services/coin-info.service';
import { Subscription, interval } from 'rxjs';

@Component({
  selector: 'app-radu-image',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './radu-image.component.html',
  styleUrls: ['./radu-image.component.css']
})
export class RaduImageComponent implements OnInit, OnDestroy {

  currentImage: string = 'assets/images/suparadu-1.jpeg';
  marketCap: number = 0;
  private subscription?: Subscription;

  constructor(private coinInfoService: CoinInfoService) {}

  ngOnInit() {
    // Update market cap and image every 30 seconds
    this.subscription = interval(30000).subscribe(() => {
      this.updateMarketCapAndImage();
    });

    // Initial update
    this.updateMarketCapAndImage();
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  private updateMarketCapAndImage() {
    this.coinInfoService.getMarketCap().subscribe(
      (marketCap: number) => {
        this.marketCap = marketCap;
        this.updateImage();
      },
      (error) => {
        console.error('Error fetching market cap:', error);
      }
    );
  }

  private updateImage() {
    if (this.marketCap < 1000000) {
      this.currentImage = 'assets/images/suparadu-1.jpeg';
    } else if (this.marketCap < 5000000) {
      this.currentImage = 'assets/images/suparadu-2.jpeg';
    } else if (this.marketCap < 10000000) {
      this.currentImage = 'assets/images/suparadu-3.jpeg';
    } else {
      this.currentImage = 'assets/images/suparadu-4.jpeg';
    }
  }
}