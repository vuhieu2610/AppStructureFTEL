import { Component, OnInit, Inject } from '@angular/core';
import { Users } from './Classes/Users';
import { BaseCondition } from './Classes/BaseCondition';
import { UsersService } from './Services/users.service';
import { ReturnResult } from './Classes/ReturnResult';
import { NzModalService } from 'ng-zorro-antd';
import { FormComponent } from './Components/form/form.component';
import { Router, NavigationEnd } from '@angular/router';
import { navItems } from './_nav';
import { DOCUMENT } from '@angular/common';



@Component({
  selector: 'body',
  template:'<router-outlet></router-outlet>',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
  users: Users[] = [];
  totalCount: number = 0;
  currentCondition: BaseCondition;
  public navItems = navItems;
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement;

  constructor(
    private usersService: UsersService,
    private modalService: NzModalService,
    private router: Router,
    @Inject(DOCUMENT) _document?: any
  ) {

    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = _document.body.classList.contains('sidebar-minimized');
    });
    this.element = _document.body;
    this.changes.observe(<Element>this.element, {
      attributes: true,
      attributeFilter: ['class']
    });
  }

  ngOnInit() {
    this.currentCondition = new BaseCondition();
    this.currentCondition.PageIndex = 1;
    this.currentCondition.PageSize = 10;
    this.getPaging(this.currentCondition);
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
  }

  getPaging(condition: BaseCondition) {
    this.usersService.getPaging(condition).subscribe((rs: ReturnResult<Users>) => {
      if (rs.hasData) {
        this.users = rs.listItem;
        this.totalCount = rs.totalCount;
      }
    });
  }

  handlePageIndexChange() {
    this.usersService.getPaging(this.currentCondition).subscribe((rs: ReturnResult<Users>) => {
      if (rs.hasData) {
        this.users = rs.listItem;
        this.totalCount = rs.totalCount;
      }
    });
  }

  openModal(isEdit: boolean, user: Users, callback) {
    let title = isEdit ? "Form chỉnh sửa" : "Form thêm mới";
    this.modalService.create({
      nzTitle: title,
      nzContent: FormComponent,
      nzOnOk: callback,
      nzComponentParams: {
        user: { ...user}
      }
    });
  }

  handleEditItem(event, user: Users) {
    const onOK = u => {
      this.usersService.update(u.user).
        subscribe((rs: ReturnResult<Users>) => {
          if (!rs.hasError) {
            this.getPaging(this.currentCondition);
          }
        });
    };
    this.openModal(true, user, onOK);
  }

  handleAddNewItem(event) {
    let newUser = new Users();
    const onOk = u => {
      this.usersService.insertSingle(u.user).
        subscribe((rs: ReturnResult<Users>) => {
          if (!rs.hasError) {
            this.getPaging(this.currentCondition);
          }
        });
    };
    this.openModal(false, newUser, onOk);
  }

  handleDeleteItem(user: Users) {
    this.modalService.confirm({
      nzTitle: "Xóa " + user.fullName,
      nzContent: "Bạn có chắc muốn xóa?",
      nzOnOk: e => {
        this.usersService.delete(user).
          subscribe((rs: ReturnResult<Users>) => {
            if (!rs.hasError) {
              this.getPaging(this.currentCondition);
            }
          });
      }
    });
  }
}
