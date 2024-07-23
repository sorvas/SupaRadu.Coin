import { TestBed } from '@angular/core/testing';

import { ClaudeApiService } from './claude-api.service';

describe('ClaudeApiService', () => {
  let service: ClaudeApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClaudeApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
