import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './Entites/user/user.component';
import { TheaterComponent } from './Entites/theater/theater.component';

import { LayoutComponent } from './Layout/layout/layout.component';
import { MovieComponent } from './Entites/movie/movie.component';
import { LoginComponent } from './Entites/user/login/login.component';
import { RegisterComponent } from './Entites/user/register/register.component';

const routes: Routes = [

  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
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
      {
        path: 'movie',
         component: MovieComponent,
         data:{
          title: 'movie'
        }
      },
      // {
      //   path: 'theaterModule',
      //   loadChildren: () => import('./Entites/theater/theater.module').then(m => m.TheaterModule)
      // },
    ]

  }


];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
