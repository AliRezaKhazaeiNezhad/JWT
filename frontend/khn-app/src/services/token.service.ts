import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class TokenService {
  private ngTempTokenPath: any;
  private ngTokenPath: any;

  constructor() {
    this.ngTempTokenPath = null;
    this.ngTokenPath = undefined;
  }

  getToken(): string {
    return localStorage.getItem('token') || "";
  }

  setToken(token: string): void {
    localStorage.setItem('token', token);
  }

  removeToken(): void {
    localStorage.removeItem('token');
  }

  getHeader(): HttpHeaders {
    var myToken = "";

    myToken = this.getToken();

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${myToken}`
    });

    return headers;
  }

}