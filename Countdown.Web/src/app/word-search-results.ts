export class WordSearchResults {
  timeTaken: number;
  words: IndividualWordResult[];
}

export class IndividualWordResult {
  word: string;
  length: number;
}
