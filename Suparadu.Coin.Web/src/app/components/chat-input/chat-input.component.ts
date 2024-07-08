import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject, Observable, timer, of } from 'rxjs';
import { map, takeWhile, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-chat-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat-input.component.html',
  styleUrl: './chat-input.component.css'
})
export class ChatInputComponent {
  userInput: string = '';
  response: string = '';
  isLoading: boolean = false;
  loadingText$: Observable<string>;
  private loadingSubject = new BehaviorSubject<boolean>(false);

  constructor() {
    this.loadingText$ = this.loadingSubject.pipe(
      switchMap(isLoading => 
        isLoading ? this.getLoadingText() : of('')
      )
    );
  }

  onSubmit() {
    if (this.userInput.trim() === '') return;

    this.isLoading = true;
    this.loadingSubject.next(true);
    this.response = '';

    // Simulate API call delay
    setTimeout(() => {
      this.response = "Well, isn't that just delightful... for everyone else.";
      this.isLoading = false;
      this.loadingSubject.next(false);
      this.userInput = ''; // Clear the input after submission
    }, 3000);
  }

  private getLoadingText(): Observable<string> {
    return timer(0, 300).pipe(
      map(i => 'Typing' + '.'.repeat(i % 4)),
      takeWhile(() => this.isLoading)
    );
  }
}
