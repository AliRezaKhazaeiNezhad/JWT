import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { LoginModel } from '../models/LoginModel';
import { Component, OnInit } from '@angular/core';
import { TokenService } from '../services/token.service';
import { AuthService } from '../services/auth-service.service';
import {FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule,FormsModule, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [
    AuthService,
    TokenService,
  ],
})
export class AppComponent implements OnInit {


  model: LoginModel = new LoginModel();

  constructor(private authService: AuthService, private tokenService: TokenService) {
    this.model.Username = "admin";
    this.model.Password = "123456";
  }

  ngOnInit(): void {

  }

  onSubmit(): void {
    this.authService.login(this.model.Username, this.model.Password)
      .subscribe(token => {
        this.tokenService.setToken(token);
      });
  }


  Signout(): void {
    this.authService.signout();
  }


  Home(): void {
    this.authService.home();
  }
}