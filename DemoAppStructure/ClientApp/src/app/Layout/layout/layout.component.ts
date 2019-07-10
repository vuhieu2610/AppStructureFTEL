import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { navItems } from 'src/app/_nav';
import { DOCUMENT } from '@angular/common';
import { UsersService } from 'src/app/Services/users.service';
import { Customer } from 'src/app/Models/customer.model';
import { CarouselConfig } from 'ngx-bootstrap/carousel';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.sass'],
  providers: [
    { provide: CarouselConfig, useValue: { interval: 1500, noPause: false } },
  ]
})
export class LayoutComponent implements OnDestroy, OnInit {
  myInterval: number | false = 2000;
  slides: any[] = [];
  image: any[] = [
    `https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/9/8/980_11.jpg`,
    `https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/s/p/spiderman_sneak_980_x_448_1.jpg`,
    `https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/h/e/he_ca_tinh_980x448.jpg`,
  ];
  activeSlideIndex: number = 0;
  noWrapSlides: boolean = false;
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
    for (let i = 0; i < 3; i++) {
      this.addSlide(i);
    }
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
    this.myInterval = 0;
    this.noWrapSlides = true;
    this.myInterval = false;
  }

  addSlide(i): void {
    this.slides.push({
      item: this.image[i]
    });
  }
  removeSlide(index?: number): void {
    const toRemove = index ? index : this.activeSlideIndex;
    this.slides.splice(toRemove, 1);
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
