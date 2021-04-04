import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUserRegister } from '../shared/models/interfaces';
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
      FirstName: ['', [Validators.required] ],
      LastName: ['', [Validators.required] ],
      Username: ['', [Validators.required, Validators.email] ],
      Password: ['', [Validators.required] ],
      PasswordConfirmation: ['', [Validators.required] ],
      Agreement: [false, [Validators.requiredTrue] ]
    }, { validators: (group: FormGroup) => {return group.controls.Password.value === group.controls.PasswordConfirmation.value ? null : {notSame: true}} });
  }

  ngOnInit(): void {
  }

  register(form: IUserRegister): void {
    // console.log(form);
    this.auth.register(form);
    this.registerForm.reset();
  }

}
