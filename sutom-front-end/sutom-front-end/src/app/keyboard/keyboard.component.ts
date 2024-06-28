import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-keyboard',
  templateUrl: './keyboard.component.html',
  styleUrls: ['./keyboard.component.scss']
})
export class KeyboardComponent {
  keys: string[][] = [
    ['Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P'],
    ['A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L'],
    ['ENTER', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', 'BACK']
  ];

  @Output() keyClick = new EventEmitter<string>();
  @Output() enterClick = new EventEmitter<void>();
  @Output() backspaceClick = new EventEmitter<void>();

  onKeyClick(key: string) {
    if (key === 'ENTER') {
      this.enterClick.emit();
    } else if (key === 'BACK') {
      this.backspaceClick.emit();
    } else {
      this.keyClick.emit(key);
    }
  }

  isSpecialKey(key: string): boolean {
    return key === 'ENTER' || key === 'BACK';
  }

}
