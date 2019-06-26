import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './Entites/user/user.component';
import { TheaterComponent } from './Entites/theater/theater.component';

const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    
  },
  {
    path: 'https://localhost:44367/api/Users/GetPaging', component: TheaterComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
