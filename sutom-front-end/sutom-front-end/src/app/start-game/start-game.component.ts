import { Component } from '@angular/core';
import { GameService } from '../services/game.service';
import { StartGameRequest } from '../models/startGameRequest';
import { tap } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.scss']
})
export class StartGameComponent {
  wordLength: number | null = null;
  maxAttempts: number | null = null;
  submitted = false;
  constructor(private gameService: GameService, private router: Router) { }

  onSubmit() {
    this.gameService.startGame(new StartGameRequest(this.wordLength, this.maxAttempts))
      .pipe(tap((game) => {
        this.router.navigate([`/game/${game.id}`]);
      })).subscribe();


  }
}
