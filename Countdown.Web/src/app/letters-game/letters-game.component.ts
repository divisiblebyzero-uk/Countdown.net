import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LettersGameService } from '../letters-game.service';
import { WordSearchResults } from '../word-search-results';

@Component({
  selector: 'app-letters-game',
  templateUrl: './letters-game.component.html',
  styleUrls: ['./letters-game.component.scss']
})

export class LettersGameComponent implements OnInit {

  constructor(private lettersGameService: LettersGameService) { }


  results: WordSearchResults = {
    timeTaken: null,
    words: null
  };

  letters = new FormControl();
  

  getSearchResults() {

    this.lettersGameService.getSearchResults(this.letters.value)
      .subscribe(data => this.results = {
        timeTaken: (data as any).timeTaken,
        words: (data as any).words
      });
  }

  ngOnInit() {
    this.letters.setValue("blah");
  }

}
