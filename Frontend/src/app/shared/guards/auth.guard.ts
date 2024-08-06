import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router:Router, private authService:AuthService) {
  }
  canActivate(){
    if (this.authService.isTokenValid()){
      return true;
    }
    this.router.navigateByUrl('/');
    return false;
  }
}
