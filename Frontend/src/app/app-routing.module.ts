import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/pages/login/login.component';
import { RegisterComponent } from './core/pages/register/register.component';
import { HomeComponent } from './core/pages/home/home.component';
import { ProductsComponent } from './core/pages/products/products.component';
import { RedirectGuard } from './shared/guards/redirect.guard';
import { AuthGuard } from './shared/guards/auth.guard';
import { AddProductComponent } from './core/pages/add-product/add-product.component';
import { EditProductComponent } from './core/pages/edit-product/edit-product.component';

const routes: Routes = [
  {path: '', component: HomeComponent, canActivate:[RedirectGuard]},
  {path: 'login', component: LoginComponent, canActivate:[RedirectGuard]},
  {path: 'register', component: RegisterComponent, canActivate:[RedirectGuard]},
  {path: 'products', component: ProductsComponent},
  {path: 'add-product', component: AddProductComponent, canActivate:[AuthGuard]},
  {path: 'edit-product/:id', component: EditProductComponent, canActivate:[AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
