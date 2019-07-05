import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.sass']
})
export class UserComponent implements OnInit {

  constructor(private _route : ActivatedRoute) { }

  ngOnInit() {
    console.log(this._route.snapshot.queryParamMap.get('role'));
    
  }

}
