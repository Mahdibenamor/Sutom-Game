export class LetterResult {
    constructor(public letter: string = '') { }
    public status: 'correct' | 'misplaced' | 'wrong' | 'empty' = 'empty';
}

export interface GuessResult {
    correct: boolean;
    showInfoMessage: boolean;
    letterResults: LetterResult[];
}