import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
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
    console.log(form);
    this.auth.register(form);
    this.registerForm.reset();
  }

}
