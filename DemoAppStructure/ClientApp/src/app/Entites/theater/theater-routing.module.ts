import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from '../user/user.component';
import { ThemMoiTheaterComponent } from './them-moi-theater/them-moi-theater.component';

const routes: Routes = [
  { path:'',
    data:{
      title:'Theater'
    },
    children:[

      {
        path: 'themMoi',
        component: ThemMoiTheaterComponent,
      }
      
    ]
  },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TheaterRoutingModule { }
