import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './Entites/user/user.component';
import { TheaterComponent } from './Entites/theater/theater.component';
import { AppComponent } from './app.component';
import { LayoutComponent } from './Layout/layout/layout.component';

const routes: Routes = [

  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '',
    component: LayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {

        path: 'user',
        component: UserComponent,
        data:{
          title: 'user'
        }

      },
      {
        path: 'theater',
         component: TheaterComponent,
         data:{
          title: 'theater'
        }
      },
    ]

  }


];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
