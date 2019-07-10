import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieComponent } from './movie.component';
import { MovieService } from 'src/app/Services/movie.service';
import { RouterModule } from '@angular/router';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { DialogModule } from '../dialog/dialog.module';
import { BookingModalComponent } from '../dialog/booking-modal/booking-modal.component';

const BOOKING_MODAL= [
  BookingModalComponent
];
@NgModule({
  declarations: [MovieComponent, MovieDetailsComponent,
  ...BOOKING_MODAL
  ],
  imports: [
    CommonModule,
    RouterModule,
    DialogModule
  ],
  providers: [MovieService]
})
export class MovieModule { }
