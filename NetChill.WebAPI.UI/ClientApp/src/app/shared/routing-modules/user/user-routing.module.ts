import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NetchillHomeComponent } from 'src/app/presentation/user/netchill-home/netchill-home.component';

const routes: Routes = [
  {
    path: '', component: NetchillHomeComponent,
    children: [
      { path: 'Home', component: NetchillHomeComponent }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
