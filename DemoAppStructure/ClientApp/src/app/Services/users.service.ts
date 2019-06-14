import { Injectable } from '@angular/core';
import { BaseCondition } from '../Classes/BaseCondition';
import { ReturnResult } from '../Classes/ReturnResult';
import { Users } from '../Classes/Users';
import { HttpClient, HttpHeaders } from '@angular/common/http';
//import { catchError, map, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private staticAPI: string = "https://localhost:44367/api/Users/";
  constructor(
    private http: HttpClient
  ) { }

  getPaging(condition: BaseCondition) {
    let url = this.staticAPI + "GetPaging"; 
    return this.http.post<ReturnResult<Users>>(url, condition);
  }

  update(user: Users) {
    let url = this.staticAPI + "UpdateSingle";
    return this.http.post<ReturnResult<Users>>(url, user);
  }

  insertSingle(user: Users) {
    let url = this.staticAPI + "InsertSingle";
    return this.http.post<ReturnResult<Users>>(url, user);
  }

  delete(user: Users) {
    let url = this.staticAPI + "Delete";
    return this.http.post<ReturnResult<Users>>(url, user);
  }

}
