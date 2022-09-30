import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faFacebook, faTwitter, faGoogle } from '@fortawesome/free-brands-svg-icons';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { Register } from 'src/app/_models/security/register.interface';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: Register = {
    email: "",
    firstName: "",
    username: "",
    lastName: "",
    middleName: null,
    password: "",
    duplicatePassword: ""
  }

  icons: any = {
    faFacebook: faFacebook,
    faTwitter: faTwitter,
    faGoogle: faGoogle,
    faSpinner: faSpinner
  }

  isRegisterInProgress = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.isRegisterInProgress = true;
    this.authService.register(this.registerForm).subscribe({
      next: data => {
        this.isRegisterInProgress = false;
        this.router.navigate(["/login"]);
      },
      error: error => {
        this.isRegisterInProgress = false;
      }
    });
  }
}
