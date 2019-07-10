import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardAdminService implements CanActivate {

  constructor(private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    var role: string = localStorage.getItem('role');
  //  console.log(route.queryParamMap.get('role'));
  
    if (!role) {
      this.router.navigate(['/'], { queryParams: { required:  'login' + state.url  } });
      return false;
    }
    var roleCurrent: string = '';
    var roles: string[] = window.location.href.split('/');
    roleCurrent = roles[roles.length - 2];
    if (role == "admin") {
      return true;
    }
    this.router.navigate(['/pagenotfound'], { queryParams: { required: state.url + 'admin' } });
    return false;
  }
}
