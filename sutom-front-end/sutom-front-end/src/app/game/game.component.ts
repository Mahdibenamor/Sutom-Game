import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Game } from '../models/game';
import { GameService } from '../services/game.service';
import { tap, catchError, of } from 'rxjs';
import { GuessRequest } from '../models/guessRequest';
import { GuessResult, LetterResult } from '../models/guessResult';
import { GameResultComponent } from '../game-result/game-result.component';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {
  currentGame: Game | undefined = undefined;
  board: LetterResult[][] = [];
  currentRow = 0;
  currentCol = 0;
  roundFinished = false;
  showInfoMessage = false;
  keys: string[][] = [
    ['Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P'],
    ['A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L'],
    ['ENTER', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', 'BACK']
  ];

  constructor(
    private activatedRoute: ActivatedRoute,
    private gameService: GameService,
    private router: Router,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    const gameIdStr = this.activatedRoute.snapshot.paramMap.get('gameId')!;
    let gameId: number;

    try {
      gameId = parseInt(gameIdStr, 10);
      if (isNaN(gameId)) {
        throw new Error('Invalid game ID');
      }
      this.getGame(gameId);
    } catch (error) {
      this.navigateToWelcome()
    }
  }


  getGame(gameId: number): void {
    this.gameService.getGameById(gameId)
      .pipe(tap((game) => {
        this.currentGame = game
        this.buildBoard(game);
      }), catchError((error) => {
        this.navigateToWelcome()
        return of(undefined);
      })).subscribe();
  }

  buildBoard(game: Game) {
    this.board = Array.from({ length: game.maxAttemps }, () => Array(game.difficulty).fill(''));
    if (this.currentGame != undefined) {
      this.currentRow = this.currentGame.guesses.length;
      for (var i = 0; i < this.currentGame!.guesses.length; i++) {
        let guess = this.currentGame.guesses[i];
        for (var j = 0; j < guess.guessResult.letterResults.length; j++) {
          this.board[i][j] = guess.guessResult.letterResults[j];
        }
      }
    }
  }

  navigateToWelcome() {
    this.router.navigate([`/welcome`]);
  }

  @HostListener('document:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    const key = event.key.toUpperCase();
    if (!this.roundFinished) {
      if (key === 'ENTER') {
        this.onEnter();
      } else if (key === 'BACKSPACE') {
        this.onBackspace();
      } else if (/^[A-Z]$/.test(key)) {
        this.onKeyClick(key);
      }
    }
  }

  onKeyClick(key: string) {
    if (this.currentGame != undefined && this.currentCol < this.currentGame.difficulty && !this.roundFinished) {
      this.board[this.currentRow][this.currentCol] = new LetterResult(key);
      this.currentCol++;
    }
  }

  onEnter() {
    if (this.currentGame != undefined && this.currentCol === this.currentGame.difficulty && !this.roundFinished) {
      this.checkGuess();
    }
  }

  onBackspace() {
    if (this.currentCol > 0 && !this.roundFinished) {
      this.currentCol--;
      this.board[this.currentRow][this.currentCol] = new LetterResult();
    }
  }

  checkGuess() {
    const guess = this.board[this.currentRow].map(l => l.letter).join('');
    if (this.currentGame != undefined) {
      let guessRequest: GuessRequest = new GuessRequest(guess);
      this.gameService.makeGuess(this.currentGame.id, guessRequest).subscribe((guessResult) => {
        if (guessResult.showInfoMessage) {
          this.showInfoPopUp();
        } else {
          this.currentRow++;
          this.currentCol = 0;
          this.ShowResultAndRestartGame(guessResult);
          this.getGame(this.currentGame!.id)
        }
      })
    }
  }

  async ShowResultAndRestartGame(guessResult: GuessResult) {
    const modalOptions: NgbModalOptions = {
      size: 'lg',
    };
    let isSucess = true;
    let shouldopen = false;
    let guess = "";
    if (this.currentGame != undefined && this.currentRow == this.currentGame?.maxAttemps) {
      shouldopen = true;
      isSucess = false;
    }
    if (guessResult.correct) {
      shouldopen = true;
      guess = guessResult.letterResults.map(l => l.letter).join('');
      isSucess = true;
    }
    if (shouldopen) {
      this.roundFinished = true
      const modalRef = this.modalService.open(GameResultComponent, modalOptions);
      modalRef.componentInstance.isSuccess = isSucess;
      modalRef.componentInstance.guess = guess;
      try {
        const result = await modalRef.result;
      } catch (error) {
      }
    }
  }

  showInfoPopUp() {
    this.showInfoMessage = true;
    setTimeout(() => {
      this.showInfoMessage = false;
    }, 3000);
  }
}
