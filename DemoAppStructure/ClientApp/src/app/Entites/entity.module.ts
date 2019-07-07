import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {UserModule} from './user/user.module';

import { TheaterModule } from './theater/theater.module';
import { MovieModule } from './movie/movie.module';
import { MovieDetailsComponent } from './movie-details/movie-details.component';

@NgModule({
  declarations: [    
  MovieDetailsComponent],
  imports: [
    CommonModule,
    UserModule,
    TheaterModule,
    MovieModule
  ]
})
export class EntityModule { }
