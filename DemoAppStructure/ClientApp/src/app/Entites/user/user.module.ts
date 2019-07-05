import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from '../user/user.component';
import { UsersService } from 'src/app/Services/users.service';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthServiceConfig, 
  GoogleLoginProvider,
   FacebookLoginProvider,
    LinkedInLoginProvider,  
    SocialLoginModule} from 'angularx-social-login';
import { FormsModule } from '@angular/forms';
import { PageNotFoundComponent } from 'src/app/Components/page-not-found/page-not-found.component';
import { AdminComponent } from './admin/admin.component';
    const config = new AuthServiceConfig([
      {
        id: GoogleLoginProvider.PROVIDER_ID,
        provider: new GoogleLoginProvider('196581055339-prbcgl26v8ffl1nfr5skn6e8juic165s.apps.googleusercontent.com')
      },
      {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider('2424435661122071')
      },
      // {
      //   id: LinkedInLoginProvider.PROVIDER_ID,
      //   provider: new LinkedInLoginProvider("78iqy5cu2e1fgr")
      // }
    ]);
    
    export function provideConfig() {
      return config;
    }
@NgModule({
  declarations: [UserComponent, LoginComponent, RegisterComponent, AdminComponent],
  imports: [
    CommonModule,
    SocialLoginModule,
    FormsModule
  ],
  providers: [
    UsersService,
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    }
  
  ]
})
export class UserModule { }
