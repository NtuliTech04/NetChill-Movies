import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { CarouselModule as OwlCarouselModule } from 'ngx-owl-carousel-o';
import { CarouselModule as PrimgengCarouselModule } from 'primeng/carousel';



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './shared/layouts/admin-layout/admin-layout.component';
import { UserLayoutComponent } from './shared/layouts/user-layout/user-layout.component';
import { AdminDashboardComponent } from './presentation/admin/dashboard/admin-dashboard/admin-dashboard.component';
import { NetchillHomeComponent } from './presentation/user/netchill-home/netchill-home.component';
import { MovieProductionComponent } from './presentation/admin/movie/movie-details/movie-production/movie-production.component';
import { MovieClipComponent } from './presentation/admin/movie/movie-details/movie-clip/movie-clip.component';
import { MovieDetailsComponent } from './presentation/admin/movie/movie-details/movie-details.component';
import { MovieGenreComponent } from './presentation/admin/common/movie-genre/movie-genre.component';
import { MovieLanguageComponent } from './presentation/admin/common/movie-language/movie-language.component';
import { MovieBaseInfoComponent } from './presentation/admin/movie/movie-details/movie-base-info/movie-base-info.component';
import { MovieBaseInfoService } from './shared/services/movie/movie-base-info.service';
import { MovieGenreService } from './shared/services/movie/accessories/movie-genre.service';
import { MovieLanguageService } from './shared/services/movie/accessories/movie-language.service';
import { MovieProductionService } from './shared/services/movie/movie-production.service';
import { MovieClipService } from './shared/services/movie/movie-clip.service';
import { DragDropDirective } from './shared/directives/drag-drop.directive';
import { ProgressComponent } from './presentation/admin/movie/movie-details/movie-clip/progress/progress.component';
import { SelectedMovieComponent } from './presentation/user/streaming/selected-movie/selected-movie.component';
import { UpcomingMoviesComponent } from './presentation/user/streaming/upcoming-movies/upcoming-movies.component';
import { SelectedUpcomingComponent } from './presentation/user/streaming/upcoming-movies/selected-upcoming/selected-upcoming.component';
import { FeaturedMoviesComponent } from './presentation/user/streaming/featured-movies/featured-movies.component';
import { LatestMoviesComponent } from './presentation/user/streaming/latest-movies/latest-movies.component';
import { CarouselUpcomingComponent } from './presentation/user/streaming/upcoming-movies/carousel-upcoming/carousel-upcoming.component';
import { CarouselFeaturedComponent } from './presentation/user/streaming/featured-movies/carousel-featured/carousel-featured.component';
import { CarouselLatestComponent } from './presentation/user/streaming/latest-movies/carousel-latest/carousel-latest.component';
import { UserSearchComponent } from './presentation/user/user-search/user-search.component';
import { SearchResultsComponent } from './presentation/user/user-search/search-results/search-results.component';
import { FilterSortComponent } from './presentation/user/filter-sort/filter-sort.component';
import { FilterSortResultsComponent } from './presentation/user/filter-sort/filter-sort-results/filter-sort-results.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    UserLayoutComponent,
    AdminDashboardComponent,
    NetchillHomeComponent,
    MovieBaseInfoComponent,
    MovieProductionComponent,
    MovieClipComponent,
    MovieDetailsComponent,
    MovieGenreComponent,
    MovieLanguageComponent,
    DragDropDirective,
    ProgressComponent,
    SelectedMovieComponent,
    UpcomingMoviesComponent,
    SelectedUpcomingComponent,
    FeaturedMoviesComponent,
    LatestMoviesComponent,
    CarouselUpcomingComponent,
    CarouselFeaturedComponent,
    CarouselLatestComponent,
    UserSearchComponent,
    SearchResultsComponent,
    FilterSortComponent,
    FilterSortResultsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    NgSelectModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    OwlCarouselModule,
    PrimgengCarouselModule
  ],
  providers: [
    // {
    //   //Adds ‘#’ in front of every route to solve the 404 Error on page reload.
    //   provide: LocationStrategy, useClass: HashLocationStrategy
    // },
    MovieGenreService,
    MovieLanguageService,
    MovieBaseInfoService,
    MovieProductionService,
    MovieClipService,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
