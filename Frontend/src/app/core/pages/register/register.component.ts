import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  showError:boolean = false
  errorMessage:string = "";
  registerForm = new FormGroup({
    username: new FormControl('',[
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(20)
    ]),
    password: new FormControl('',[
      Validators.required,
      Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}')
    ])
  });

  constructor(private router:Router, private authService: AuthService) {}

  
  onInputFocus() {
    this.errorMessage = "";
  }

  submit(){
    if (this.registerForm.invalid){
      this.showError = true;
    }
    else
    {
      this.showError = false
      const username = this.registerForm.value.username ?? ""
      const password = this.registerForm.value.password ?? ""
      this.authService.register(username, password).subscribe(result => {
        this.router.navigateByUrl("/products");
      }, (error:HttpErrorResponse) => {
        console.log(error);
        this.showError = true;
        this.errorMessage = error.error;
      });
    }
  }
}
