import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './core/pages/login/login.component';
import { RegisterComponent } from './core/pages/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './core/pages/home/home.component';
import { AuthService } from './core/services/auth.service';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { ProductsComponent } from './core/pages/products/products.component';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { JwtTokenInterceptor } from './shared/interceptors/token.interceptor';
import { ProductsService } from './core/services/products.service';
import { ProductConverterService } from './core/services/product-conveter.service';
import { ComponentsModule } from './core/components/components.module';
import { CategoriesService } from './core/services/categories.service';
import { CategoryConverterService } from './core/services/category-converter.service';
import { AddProductComponent } from './core/pages/add-product/add-product.component';
import { EditProductComponent } from './core/pages/edit-product/edit-product.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ProductsComponent,
    AddProductComponent,
    EditProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ComponentsModule
  ],
  providers: [
    provideHttpClient(withInterceptorsFromDi()),
    AuthService,
    JwtHelperService,
    {provide: JWT_OPTIONS, useValue: JWT_OPTIONS},
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtTokenInterceptor,
      multi: true
    },
    ProductsService,
    ProductConverterService,
    CategoriesService,
    CategoryConverterService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
