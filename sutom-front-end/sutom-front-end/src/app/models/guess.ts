import { GuessResult } from "./guessResult";

export interface Guess {
    id: number;
    gameId: number;
    playerGuess: string;
    guessTime: string;
    guessResult: GuessResult;
}
