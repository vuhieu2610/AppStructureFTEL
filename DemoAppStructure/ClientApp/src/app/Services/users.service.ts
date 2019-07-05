import { Injectable, OnInit } from '@angular/core';
import { BaseCondition } from '../Classes/BaseCondition';
import { ReturnResult } from '../Classes/ReturnResult';
import { Users } from '../Classes/Users';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Customer } from '../Models/customer.model';
import { User } from '../Models/user.model';
//import { catchError, map, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UsersService implements OnInit{
  
  private staticAPI: string = "https://localhost:44367/api/Users/";
  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    
  }

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

  getValue() {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Methods": "*",
        // "Access-Control-Allow-Headers": "X-PINGOTHER,Content-Type,X-Requested-With,accept,Origin,Access-Control-Request-Method,Access-Control-Request-Headers,Authorization",
        "Access-Control-Allow-Headers": "*",
        "X-Content-Type-Options": "nosniff",
        "Access-Control-Expose-Headers": "xsrf-token"
      }),
    };

    return this.http.get("https://localhost:44311/api/values", { responseType: "text" });
  }

  getCus() {
    return this.http.get<Customer>("https://localhost:44311/user");
  }

  login(username: string, password: string) {
    const httpOptions: { headers; observe } = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        // "Access-Control-Allow-Origin": "*",
        // "Access-Control-Allow-Methods": "*",
        // // "Access-Control-Allow-Headers": "X-PINGOTHER,Content-Type,X-Requested-With,accept,Origin,Access-Control-Request-Method,Access-Control-Request-Headers,Authorization",
        // "Access-Control-Allow-Headers": "*",
        // "X-Content-Type-Options": "nosniff",
        // "Access-Control-Expose-Headers": "xsrf-token"
      }),
      observe: "response"
    };
    return this.http.post<User>("https://localhost:44311/account/login", { username, password }, httpOptions);
  }

  sendRequest() {
    // this._http.request()
  }
}
