import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { CarouselModule } from 'ngx-owl-carousel-o';


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
    SelectedMovieComponent
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
    CarouselModule
  ],
  providers: [
    {
      //Adds ‘#’ in front of every route to solve the 404 Error on page reload.
      provide: LocationStrategy, useClass: HashLocationStrategy
    },
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
