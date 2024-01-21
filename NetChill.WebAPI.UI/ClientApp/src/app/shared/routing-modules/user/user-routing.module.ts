import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NetchillHomeComponent } from 'src/app/presentation/user/netchill-home/netchill-home.component';
import { SelectedMovieComponent } from 'src/app/presentation/user/streaming/selected-movie/selected-movie.component';

const routes: Routes = [
  {
    path: '', component: NetchillHomeComponent,
    children: [
      { path: 'Home', component: NetchillHomeComponent },
      { path: 'SelectedMovie/:id', component: SelectedMovieComponent }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
