import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-game-result',
  templateUrl: './game-result.component.html',
  styleUrls: ['./game-result.component.scss']
})
export class GameResultComponent {
  isSuccess: boolean = true;
  guess: string = "";
  constructor(private router: Router, public modalService: NgbActiveModal) { }
  restartGame() {
    this.modalService.close({});
    this.router.navigate([`/welcome`]);

  }
}
