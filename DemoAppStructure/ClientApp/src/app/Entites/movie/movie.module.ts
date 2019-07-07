import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieComponent } from './movie.component';
import { MovieService } from 'src/app/Services/movie.service';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [MovieComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  providers: [MovieService]
})
export class MovieModule { }
