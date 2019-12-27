import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WordSearchResults } from "./word-search-results";

@Injectable({
  providedIn: 'root'
})
export class LettersGameService {
  private baseUrl = 'https://localhost:44389/api/WordSearch';
  constructor(private http: HttpClient) {

  }

  getSearchResults(letters: string): Observable<WordSearchResults> {
    return this.http.get(this.baseUrl + '/' + letters);
  }
}
