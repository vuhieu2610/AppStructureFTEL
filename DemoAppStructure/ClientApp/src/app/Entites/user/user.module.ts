import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from '../user/user.component';
import { UsersService } from 'src/app/Services/users.service';

@NgModule({
  declarations: [UserComponent],
  imports: [
    CommonModule
  ],
  providers: [UsersService]
})
export class UserModule { }
