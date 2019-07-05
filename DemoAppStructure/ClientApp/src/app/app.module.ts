import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgZorroAntdModule, NZ_I18N, en_US } from 'ng-zorro-antd';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormComponent } from './Components/form/form.component';
import { EntityModule } from './Entites/entity.module';
import {MDBBootstrapModule} from 'angular-bootstrap-md'

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import {
  AppAsideModule,
  AppBreadcrumbModule,
  AppHeaderModule,
  AppFooterModule,
  AppSidebarModule,
} from '@coreui/angular';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { LayoutComponent } from './Layout/layout/layout.component';
import { JwtInterceptor } from './Services/jwt.interceptor';
import { UsersService } from './Services/users.service';
import { AuthGuardService } from './Services/auth-guard.service';
import { AuthenticationService } from './Services/authentication.service';
import { DeactivateGuardService } from './Services/deactivate-guard.service';
import { AuthGuardAdminService } from './Services/auth-guard-admin.service';
import { AuthGuardUserService } from './Services/auth-guard-user.service';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';

registerLocaleData(en);
const APP_CONTAINERS = [
  LayoutComponent
];
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    AppComponent,
    FormComponent,
    LayoutComponent,
    ...APP_CONTAINERS,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgZorroAntdModule,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
  
    BrowserAnimationsModule,
    EntityModule,
    ReactiveFormsModule,
    MDBBootstrapModule.forRoot()
  ],
  entryComponents: [FormComponent],
  providers: [{ provide: NZ_I18N, useValue: en_US }, 
    UsersService, AuthGuardService, AuthenticationService, DeactivateGuardService, AuthGuardAdminService,
    AuthGuardUserService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
