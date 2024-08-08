import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  animations: [
    trigger('scaleOnHover', [
      state('normal', style({
        transform: 'scale(1)'
      })),
      state('hovered', style({
        transform: 'scale(1.1)'
      })),
      transition('normal <=> hovered', animate('200ms ease-in-out'))
    ])
  ]
})
export class HeaderComponent {
  navItems = ['ABOUT', 'HOW TO BUY', 'TALK TO SUPARADU', 'TOKENOMICS', 'ROADMAP'];
  logoState = 'normal';
  navItemStates: { [key: string]: 'normal' | 'hovered' } = {};

  setHoverState(item: string, isHovered: boolean): void {
    if (item === 'logo') {
      this.logoState = isHovered ? 'hovered' : 'normal';
    } else {
      this.navItemStates[item] = isHovered ? 'hovered' : 'normal';
    }
  }

  getNavItemState(item: string): 'normal' | 'hovered' {
    return this.navItemStates[item] || 'normal';
  }

  getScrollToId(item: string): string {
    return item.toLowerCase().replace(/ /g, '-');
  }

  scrollTo(elementId: string): void {
    const element = document.getElementById(elementId);
    if (element) {
      const headerOffset = 60; // Adjust this to match your header's height
      const elementPosition = element.getBoundingClientRect().top;
      const offsetPosition = elementPosition + window.scrollY - headerOffset;

      window.scrollTo({
        top: offsetPosition,
        behavior: 'smooth'
      });
    }
  }
}