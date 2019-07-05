import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardUserService implements CanActivate {
  constructor(private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    var role: string = localStorage.getItem('role');
    if (role === "user") {
      return true;
    }
    this.router.navigate(['/pagenotfound'], { queryParams: { required: state.url + 'user' } });
    return false;
  }
}
