import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { BehaviorSubject, Subscription } from 'rxjs';
import { UserRole } from '../Models/user-role.model';

@Injectable({
  providedIn: 'root'
})

export class AuthGuardService implements CanActivate {
  constructor(private _router: Router, private _auth: AuthenticationService) {
    _auth.currentUserSubject = new BehaviorSubject<UserRole>(JSON.parse(localStorage.getItem('userCurrent')));
    _auth.currentUser = _auth.currentUserSubject.asObservable();

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    // const currentUser = this._auth.currentUserValue;
    // const roles: string[] = route.data.role;
    // if (currentUser) {
    //   for (let i = 0; i < currentUser.roles.length; i++) {
    //     for (let j = 0; j < roles.length; j++) {
    //       if (currentUser.roles[i] == roles[j]) {
    //         localStorage.setItem('role', JSON.stringify(currentUser[i]));
    //      //   this._router.navigate['/'];
    //         break;
    //       }
    //     }
    //   }
    //   return true;
    // }
    // else if (currentUser == null) {
    //   return true;
    // }
    // this._router.navigate(['/login']);
    // return false;
 //   const roles: string[] = route.data.role;
    const token: string = localStorage.getItem('token');
    const role: string = localStorage.getItem('role');
    if (token && role) {
      // for (var item of roles) {
      //   if (item.search(role) > -1) {
      //     break;
      //   }
      // }
      return true;
    }
    else if (!token) {
      //  this._router.navigate(['/login'], { queryParams:{ requied: state.url }});
      return true;
    }
    this._router.navigate(['/login']);
    return false;
  }
}
