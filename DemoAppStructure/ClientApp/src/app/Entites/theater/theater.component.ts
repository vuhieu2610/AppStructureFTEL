import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-theater',
  templateUrl: './theater.component.html',
  styleUrls: ['./theater.component.sass']
})
export class TheaterComponent implements OnInit {

  constructor() { }

 
 
  ngOnInit() {
  }

  clickRout(){
    console.log("routing ok ");
  }
  
}
