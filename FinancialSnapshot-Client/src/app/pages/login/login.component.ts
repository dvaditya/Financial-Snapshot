import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { faFacebook, faTwitter, faGoogle } from '@fortawesome/free-brands-svg-icons';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { Login } from 'src/app/_models/security/login.interface';
import { ApiResponse } from 'src/app/_models/apiResponse.interface';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: Login = { username: "", password: "" };

  icons: any = {
    faFacebook: faFacebook,
    faTwitter: faTwitter,
    faGoogle: faGoogle,
    faSpinner: faSpinner
  }

  isLoginInProgress = false;
  isLoginFailed = false;
  saveCreds = false;
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    // Check if token exists
  }

  onSubmit(): void {
    this.isLoginInProgress = true;
    this.authService.login(this.loginForm).subscribe({
      next: data => {
        this.isLoginFailed = false;
        this.isLoginInProgress = false;
        this.router.navigate(["/home"]);
      },
      error: error => {
        this.isLoginInProgress = false;
        this.errorMessage = error.message;
        this.isLoginFailed = true;
      }
    });
  }

}
