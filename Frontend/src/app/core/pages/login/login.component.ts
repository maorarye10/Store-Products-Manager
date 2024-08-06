import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm = new FormGroup({
    username: new FormControl('',[
      Validators.required
    ]),
    password: new FormControl('',[
      Validators.required
    ])
  })
  error:string = ""

  constructor(private router:Router, private authService: AuthService) {}

  onInputFocus() {
    this.error = "";
  }

  submit(){
    if (this.loginForm.invalid){
      this.error = "Username and password are required fields"
    }
    else{
      const username = this.loginForm.value.username ?? ""
      const password = this.loginForm.value.password ?? ""
      this.authService.login(username, password).subscribe((data => {
        this.router.navigateByUrl("/products");
      }), (error:HttpErrorResponse) => {
        console.log(error.message);
        this.error = error.message;
      })
    }
  }
}
