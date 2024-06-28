import { Guess } from "./guess";

export interface Game {
    id: number;
    word: string;
    date: string;
    guesses: Guess[];
    maxAttemps: number;
    difficulty: number;
}