import { Component, OnInit, Input } from '@angular/core';
import { Users } from '../../Classes/Users';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.sass']
})
export class FormComponent implements OnInit {
  @Input() user: Users;
  constructor() { }

  ngOnInit() {
  }

  handleInputChange(e) {
    const { value, name } = e.target;
    this.user[name] = value;
  }
}
