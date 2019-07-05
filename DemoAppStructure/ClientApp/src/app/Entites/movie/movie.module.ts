import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieComponent } from './movie.component';
import { MovieService } from 'src/app/Services/movie.service';

@NgModule({
  declarations: [MovieComponent],
  imports: [
    CommonModule,
  ],
  providers: [MovieService]
})
export class MovieModule { }
