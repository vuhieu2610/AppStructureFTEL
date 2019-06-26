import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {UserModule} from './user/user.module';

import { TheaterModule } from './theater/theater.module';

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    UserModule,
    TheaterModule
  ]
})
export class EntityModule { }
