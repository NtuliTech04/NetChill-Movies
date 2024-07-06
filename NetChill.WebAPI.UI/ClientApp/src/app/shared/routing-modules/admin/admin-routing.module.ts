import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieGenreComponent } from 'src/app/presentation/admin/common/movie-genre/movie-genre.component';
import { MovieLanguageComponent } from 'src/app/presentation/admin/common/movie-language/movie-language.component';

import { AdminDashboardComponent } from 'src/app/presentation/admin/dashboard/admin-dashboard/admin-dashboard.component';
import { MovieBaseInfoComponent } from 'src/app/presentation/admin/movie/movie-details/movie-base-info/movie-base-info.component';
import { MovieClipComponent } from 'src/app/presentation/admin/movie/movie-details/movie-clip/movie-clip.component';
import { MovieProductionComponent } from 'src/app/presentation/admin/movie/movie-details/movie-production/movie-production.component';

const routes: Routes = [
  {
    path: '', component: AdminDashboardComponent,
    children: [
      { path: 'Dashboard', component: AdminDashboardComponent },
      { path: 'CreateGenre', component: MovieGenreComponent },
      { path: 'CreateLanguage', component: MovieLanguageComponent },
      { path: 'MovieBaseInfo', component: MovieBaseInfoComponent },
      { path: 'MovieProduction/:id', component: MovieProductionComponent },
      { path: 'MovieMedia/:id', component: MovieClipComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
