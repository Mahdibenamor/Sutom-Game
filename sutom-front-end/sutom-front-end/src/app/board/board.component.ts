import { Component, Input } from '@angular/core';
import { LetterResult } from '../models/guessResult';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent {
  @Input() board: LetterResult[][] = [];

}
