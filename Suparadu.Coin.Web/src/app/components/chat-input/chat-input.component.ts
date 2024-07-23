import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject, Observable, timer, of } from 'rxjs';
import { map, takeWhile, switchMap, finalize } from 'rxjs/operators';
import { ClaudeApiService } from '../../services/claude-api.service';

@Component({
  selector: 'app-chat-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat-input.component.html',
  styleUrl: './chat-input.component.css'
})
export class ChatInputComponent {
  private claudeApiService = inject(ClaudeApiService);

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

    this.claudeApiService.getNegativeReply(this.userInput)
      .pipe(
        finalize(() => {
          this.isLoading = false;
          this.loadingSubject.next(false);
          this.userInput = ''; // Clear the input after submission
        })
      )
      .subscribe({
        next: (result) => {
          this.response = result.response;
        },
        error: (error) => {
          console.error('Error from Claude API:', error);
          this.response = 'Sorry, I encountered an error while processing your request.';
        }
      });
  }

  private getLoadingText(): Observable<string> {
    return timer(0, 300).pipe(
      map(i => 'Typing' + '.'.repeat(i % 4)),
      takeWhile(() => this.isLoading)
    );
  }
}
