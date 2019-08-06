import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'angularx-social-login';
import { SocialUser } from 'angularx-social-login';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GoogleLoginProvider, FacebookLoginProvider, LinkedInLoginProvider } from 'angularx-social-login';
import { UsersService } from 'src/app/Services/users.service';
import { Customer } from 'src/app/Models/customer.model';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  user: SocialUser;
  username: string = '';
  password: string = '';
  loading = false;
  submitted = false;
  error: string;
  cus: string;
  alertFailed: string;

  constructor(
    private router: Router,
    private authService: AuthService,
    //   private formBuilder: FormBuilder,
    private userService: UsersService
  ) {
    this.username = '';
    this.password = '';
    this.alertFailed = '';
  }

  ngOnInit() {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      console.log(user);
    });
    this.cus = localStorage.getItem('user');
    // this.loginForm = this.formBuilder.group({
    //   username: ['', Validators.required],
    //   password: ['', Validators.required]
    // });
    // console.log(this.username, this.password);
    // this.userService.getValue()
    //   .subscribe((response) => console.log(response));
  }
  clear() {
    this.username = '';
    this.password = '';
    // setInterval(() => {this.alertFailed = ''}, 3000);
  }

  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID).then(x => console.log(x));
  }


  signInWithFB(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(x => console.log(x));
  }

  signInWithLinkedIn(): void {
    this.authService.signIn(LinkedInLoginProvider.PROVIDER_ID).then(x => console.log(x));
  }

  signOut(): void {
    this.authService.signOut();
  }
  register() {
    this.router.navigate(['/register']);
  }

  onSubmit() {
    console.log(this.username, this.password);
    try {
      this.userService.login(this.username, this.password)
        .subscribe((response) => {
          localStorage.setItem('role', response['body']['roles']);
          localStorage.setItem('token', response['body']['accessToken']);
          localStorage.setItem('user', this.username);
          this.router.navigate(['/'], { queryParams: { role: response['body']['roles'] } });
        }, (error) => {
          console.log(error.message);
          this.error = error.message;
          this.alertFailed = 'Sai tên đăng nhập hoặc mật khẩu';
          //  this.clear();
        }, () => { this.error = null; });
    } catch (error) {
      console.log(error);
    }
  }

  textChange(event) {
    if (this.error) {
      console.log(this.error);
      this.alertFailed = '';
    }
  }
}
