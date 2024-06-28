import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GameComponent } from './game/game.component';
import { KeyboardComponent } from './keyboard/keyboard.component';
import { BoardComponent } from './board/board.component';
import { StartGameComponent } from './start-game/start-game.component';
import { NgbActiveModal, NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { WelcomeComponent } from './welcome/welcome.component';
import { GameResultComponent } from './game-result/game-result.component';

@NgModule({
  declarations: [
    AppComponent,
    GameComponent,
    KeyboardComponent,
    BoardComponent,
    StartGameComponent,
    WelcomeComponent,
    GameResultComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    NgbModal,
    NgbActiveModal
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
