import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { navItems } from 'src/app/_nav';
import { DOCUMENT } from '@angular/common';
import { UsersService } from 'src/app/Services/users.service';
import { Customer } from 'src/app/Models/customer.model';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.sass']
})
export class LayoutComponent implements OnDestroy, OnInit {
  public role: string;
  public user: string;
  public navItems = navItems;
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement;
  public cus: Customer;
  constructor(private userService: UsersService, @Inject(DOCUMENT) _document?: any) {

    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = _document.body.classList.contains('sidebar-minimized');
    });
    this.element = _document.body;
    this.changes.observe(<Element>this.element, {
      attributes: true,
      attributeFilter: ['class']
    });
    this.cus = new Customer();
  }
  ngOnInit(): void {
    this.user = localStorage.getItem('user');
    this.role = localStorage.getItem('role');
    this.userService.getCus().subscribe((customer) => {
      this.cus = customer;
      console.log(customer);
    });
  }

  ngOnDestroy(): void {
    this.changes.disconnect();
  }

  logOut() {
    localStorage.removeItem('role');
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.user = "No account logged";
    this.role = null;
    this.cus = null;
  }
}
