import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUserLogin } from '../shared/models/interfaces';
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
      username: ['', [Validators.required, Validators.email] ],
      password: ['', [Validators.required] ],
    });
  }

  ngOnInit(): void {
  }

  login(form: IUserLogin): void {
    // console.log(form);
    this.auth.login(form);
    this.loginForm.reset();
  }

}
