import { trigger, transition, style, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [
    trigger('slideInOut', [
      transition(':enter', [
        style({ transform: 'translateY(10%)' }),
        animate('.5s cubic-bezier(0.33, 1, 0.68, 1)', style({ transform: 'translateY(0)' }))
      ]),
      transition(':leave', [
        style({ transform: 'translateY(0)' }),
        animate('.5s cubic-bezier(0.33, 1, 0.68, 1)', style({ transform: 'translateY(10%)' }))
      ])
    ])
  ],
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService) {
    this.loginForm = this.fb.group({
      // Temporary values for easy loging
      email: ['test@example.com', [Validators.required, Validators.email] ],
      password: ['DSAewq321%', [Validators.required] ],
    });
  }

  ngOnInit(): void {
  }

  login(form: any): void {
    this.auth.login(form);
  }

}
