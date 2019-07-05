import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UsersService } from './users.service';
import { Router } from '@angular/router';
import { UserRole } from '../Models/user-role.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  currentUserSubject: BehaviorSubject<UserRole>;
  currentUserRole: BehaviorSubject<UserRole>;
  userRole: Observable<string[]>;
  currentUser: Observable<UserRole>;

  constructor(private _userService: UsersService, private _router: Router, _httpClient: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<UserRole>(JSON.parse(localStorage.getItem('userCurrent')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue() {
    return this.currentUserSubject.value;
  }

  public login(username: string, password: string) {
    // return this._userService.getData()
    //   .pipe(map(data => {
    //     for (let item of data) {
    //       //  if()
    //       if (username == item.username && password == item.password) {
    //         localStorage.setItem('userCurrent', JSON.stringify(item));
    //         this.currentUserSubject.next(item);
    //         this._router.navigate(['/']);
    //         break;
    //       }
    //     }
    //     return new Observable<any>();
    //   }, () => console.log("load successfully")
    //   ));
  }

  // logout
  public logout() {
    localStorage.removeItem('userCurrent');
    this.currentUserSubject.next(null);
  }

  public isActivate(role: string[]){
    //user = JSON.parse(localStorage.getItem('userRole'));
    
  }
}
