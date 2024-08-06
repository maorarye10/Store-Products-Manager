import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserDTO } from '../DTOs/user/user-dto';
import { catchError, tap, throwError } from 'rxjs';
import { ServerUserDTO } from '../DTOs/user/server-user-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _userApiUrl:string = environment.API_URL + "/users";
  private _jwtToken: string = "";
  user:ServerUserDTO = {
    id: 0,
    username: "",
    role: "",
    lastLogin: Date.now(),
    token: ""
  };


  constructor(private httpClient:HttpClient, private jwtHelper: JwtHelperService) { }

  login(username:string, password:string){
    const userDTO:UserDTO = {
      username,
      password
    };

    return this.httpClient.post<ServerUserDTO>(`${this._userApiUrl}/login`, userDTO).pipe(
      tap(data => {
        this.addToken(data)
      }),
      catchError(this.handleError)
    )
  }

  register(username:string, password:string){
    const userDTO:UserDTO = {
      username,
      password
    };
    return this.httpClient.post<ServerUserDTO>(`${this._userApiUrl}/register`, userDTO).pipe(
      tap(data => {
        this.addToken(data)
      })
    )
  }

  isTokenValid(): boolean {
    this._jwtToken = localStorage.getItem("jwt_token") ?? "";

    if (!this._jwtToken){
      return false;
    }

    const isTokenExpired = this.jwtHelper.isTokenExpired(this._jwtToken);

    if (isTokenExpired){
      this.deleteToken();
      return false;
    }

    return true;
  }

  logOut() {
    this.deleteToken();
  }

  private addToken(userDTO: ServerUserDTO) {
    this.user = userDTO;
    this._jwtToken = userDTO.token;
    localStorage.setItem('jwt_token', this._jwtToken);
  }
  private deleteToken() {
    this._jwtToken = "";
    localStorage.removeItem("jwt_token");
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 401) {
      return throwError(() => new Error('Invalid username or password'));
    } 
    if (error.status === 422){
      return throwError(() => new Error('Invalid DTO'));
    }
    // Return an observable with a user-facing error message.
    console.log(error.error);
    return throwError(() => new Error(`Something bad happened; please try again later. Error: ${error.error}`));
  }
}
