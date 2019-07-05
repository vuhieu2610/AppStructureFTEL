import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';


export interface CanComponentDeactivate{
  isDeactivate() : boolean;
}
@Injectable({
  providedIn: 'root'
})
export class DeactivateGuardService implements CanDeactivate<CanComponentDeactivate>{
  

  constructor() { }

  canDeactivate(component: CanComponentDeactivate): boolean {
    // this.user = JSON.parse(localStorage.getItem('userRole'));
    // console.log(this.user);
    // return component.isDeactivate();
    return true;
  }
}
