import {ApplicationConfig, provideZoneChangeDetection} from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import {provideRouter} from '@angular/router';
import {provideHttpClient} from '@angular/common/http';
import {routes} from './app.routes';

import {CoinInfoService} from './services/coin-info.service';
import {ClaudeApiService} from "./services/claude-api.service";

export const appConfig: ApplicationConfig = {
  providers:
    [
      provideZoneChangeDetection({eventCoalescing: true}),
      provideRouter(routes),
      provideHttpClient(),
      provideAnimations(),
      ClaudeApiService,
      CoinInfoService
    ]
};
