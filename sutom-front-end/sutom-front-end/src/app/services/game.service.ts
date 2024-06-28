import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from '@angular/common/http';
import { Game } from "../models/game";
import { GuessResult } from "../models/guessResult";
import { StartGameRequest } from "../models/startGameRequest";
import { GuessRequest } from "../models/guessRequest";

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private apiUrl = 'http://localhost:5173/game';

  constructor(private http: HttpClient) { }

  startGame(startGameRequest: StartGameRequest): Observable<Game> {
    return this.http.post<Game>(this.apiUrl, startGameRequest);
  }

  makeGuess(gameId: number, guess: GuessRequest): Observable<GuessResult> {
    return this.http.post<GuessResult>(`${this.apiUrl}/${gameId}/guess`, guess);
  }

  getGameById(gameId: number): Observable<Game> {
    return this.http.get<Game>(`${this.apiUrl}/${gameId}`);
  }
}