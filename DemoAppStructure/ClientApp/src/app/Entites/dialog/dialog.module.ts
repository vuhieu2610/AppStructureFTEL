import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DialogRoutingModule } from './dialog-routing.module';
import { BookingModalComponent } from './booking-modal/booking-modal.component';
import { ModalModule } from 'ngx-bootstrap';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    DialogRoutingModule,
    ModalModule.forRoot(),
  ]
})
export class DialogModule { }
