import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { TokenService } from './token.service';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { LoginResponseModel } from '../models/LoginResponseModel';

@Injectable()


export class AuthService {
  private apiUrl = 'http://localhost:33464/Account/';
  private model: LoginResponseModel = new LoginResponseModel();

  constructor(private http: HttpClient, private tokenService: TokenService) { }

  login(username: string, password: string): Observable<string> {
    return this.http.post(this.apiUrl + "Login", { username, password })
      .pipe(map((response: any) => {
        this.tokenService.setToken(response.token);
        return response.token;
      }));
  }


  signout(): void {
    localStorage.clear();
  }

  home(): Observable<any> {
    const header = this.tokenService.getHeader();

    return this.http.get(this.apiUrl + "home", { headers: header })
      .pipe(
        map(response => response),
        catchError(error => {
          console.error(error);
          return error;
        })
      );
  }

}

