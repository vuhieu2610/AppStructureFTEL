import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TheaterRoutingModule } from './theater-routing.module';
import { UserComponent } from '../user/user.component';
import { TheaterComponent } from './theater.component';
import { ThemMoiTheaterComponent } from './them-moi-theater/them-moi-theater.component';

@NgModule({
  declarations: [
    
    TheaterComponent,
    
    ThemMoiTheaterComponent
  ],
  imports: [
    CommonModule,
    TheaterRoutingModule
  ]
})
export class TheaterModule { }
