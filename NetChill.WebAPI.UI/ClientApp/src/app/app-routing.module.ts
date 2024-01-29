import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminLayoutComponent } from './shared/layouts/admin-layout/admin-layout.component';
import { UserLayoutComponent } from './shared/layouts/user-layout/user-layout.component';
import { MovieBaseInfoComponent } from './presentation/admin/movie/movie-details/movie-base-info/movie-base-info.component';
import { MovieGenreComponent } from './presentation/admin/common/movie-genre/movie-genre.component';
import { MovieLanguageComponent } from './presentation/admin/common/movie-language/movie-language.component';
import { MovieProductionComponent } from './presentation/admin/movie/movie-details/movie-production/movie-production.component';
import { MovieClipComponent } from './presentation/admin/movie/movie-details/movie-clip/movie-clip.component';
import { SelectedMovieComponent } from './presentation/user/streaming/selected-movie/selected-movie.component';
import { SelectedUpcomingComponent } from './presentation/user/streaming/upcoming-movies/selected-upcoming/selected-upcoming.component';
import { CarouselFeaturedComponent } from './presentation/user/streaming/featured-movies/carousel-featured/carousel-featured.component';

const routes: Routes = [
  //User Routes
  {
    path: '',
    component: UserLayoutComponent, //Loads the User layout for this Route
    children: [ //The children will also inherit the User layout
      {
        path: '', // If path is empty
        redirectTo: '/Home', //Redirect to 'home' path
        pathMatch: 'full'
      },
      {
        path: 'Home', //If path is home then...
        loadChildren: () => //Load the correct/matching Component from the UserRoutingModule which is imported by UserModule.
              import('./shared/routing-modules/user/user.module')
              .then(m=>m.UserModule)
      },
      { path: 'SelectedMovie/:id', component: SelectedMovieComponent },
      { path: 'SelectedUpcoming/:id', component: SelectedUpcomingComponent }
    ]
  },



  //Admin Routes
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/Dashboard',
        pathMatch: 'full'
      },
      {
        path: 'Dashboard',
        loadChildren: () =>
              import('./shared/routing-modules/admin/admin.module')
              .then(m => m.AdminModule)
      },
      { path: 'CreateGenre', component: MovieGenreComponent},
      { path: 'CreateLanguage', component: MovieLanguageComponent },
      { path: 'MovieBaseInfo', component: MovieBaseInfoComponent},
      { path: 'MovieProduction/:id', component: MovieProductionComponent },
      { path: 'MovieMedia/:id', component: MovieClipComponent }
    ]
  }
];

@NgModule({
  imports: [    
    RouterModule.forRoot(routes, { 
      useHash: true, //Route Hashing
      scrollPositionRestoration: 'enabled',
      anchorScrolling: 'enabled'
    }) 
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
