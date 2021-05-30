import { trigger, transition, style, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
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
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService) {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required] ],
      lastName: ['', [Validators.required] ],
      email: ['', [Validators.required, Validators.email] ],
      password: ['', [Validators.required] ],
      passwordConfirmation: ['', [Validators.required] ],
      agreement: [false, [Validators.requiredTrue] ]
    }, { validators: (group: FormGroup) => {return group.controls.password.value === group.controls.passwordConfirmation.value ? null : {notSame: true}} });
  }

  ngOnInit(): void {
  }

  register(form: any): void {
    this.auth.register(form);
  }

}
