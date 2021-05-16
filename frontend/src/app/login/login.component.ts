import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService) {
    this.loginForm = this.fb.group({
      // Temporary values for easy loging
      email: ['jan@kowalski.com', [Validators.required, Validators.email] ],
      password: ['Password123$', [Validators.required] ],
    });
  }

  ngOnInit(): void {
  }

  login(form: any): void {
    this.auth.login(form);
  }

}
