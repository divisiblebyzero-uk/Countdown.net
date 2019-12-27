import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LettersGameComponent } from './letters-game/letters-game.component';
import { NumbersGameComponent } from './numbers-game/numbers-game.component';

const routes: Routes = [
  {
    path: "letters-game",
    component: LettersGameComponent,
    data: { title: 'Letters game solver'}
  },
  {
    path: "numbers-game",
    component: NumbersGameComponent,
    data: { title: 'Numbers game solver' }
  }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
