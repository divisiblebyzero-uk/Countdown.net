import { TestBed } from '@angular/core/testing';

import { LettersGameService } from './letters-game.service';

describe('LettersGameService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LettersGameService = TestBed.get(LettersGameService);
    expect(service).toBeTruthy();
  });
});
