import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NetchillHomeComponent } from 'src/app/presentation/user/netchill-home/netchill-home.component';
import { CarouselFeaturedComponent } from 'src/app/presentation/user/streaming/featured-movies/carousel-featured/carousel-featured.component';
import { SelectedMovieComponent } from 'src/app/presentation/user/streaming/selected-movie/selected-movie.component';
import { SelectedUpcomingComponent } from 'src/app/presentation/user/streaming/upcoming-movies/selected-upcoming/selected-upcoming.component';

const routes: Routes = [
  {
    path: '', component: NetchillHomeComponent,
    children: [
      { path: 'Home', component: NetchillHomeComponent },
      { path: 'SelectedMovie/:id', component: SelectedMovieComponent },
      { path: 'SelectedUpcoming/:id', component: SelectedUpcomingComponent },
    ]
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
