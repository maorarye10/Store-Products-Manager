import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  authenticated:boolean = false
  constructor(private authService:AuthService) {}

  ngOnInit(): void {
    this.authenticated = this.authService.isTokenValid();
  }
}
