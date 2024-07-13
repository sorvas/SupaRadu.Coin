import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ChatInputComponent } from './components/chat-input/chat-input.component';
import { RaduImageComponent } from './components/radu-image/radu-image.component';
import { AboutComponent } from './components/about/about.component';
import { TokenomicsComponent } from './components/tokenomics/tokenomics.component';
import { RoadmapComponent } from './components/roadmap/roadmap.component';
import { HowtobuyComponent } from './components/howtobuy/howtobuy.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
    AboutComponent,
    ChatInputComponent,
    RaduImageComponent,
    TokenomicsComponent,
    RoadmapComponent,
    HowtobuyComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Suparadu.Coin.Web';
}
