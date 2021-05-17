import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-workers',
  templateUrl: './workers.component.html',
  styleUrls: ['./workers.component.scss']
})
export class WorkersComponent implements OnInit {

  title: string = 'Workers';
  workers = [
    {
      id: 1,
      firstName: 'John',
      lastName: 'Smith',
      role: 'Owner',
      joined: '02/03/21'
    },
    {
      id: 2,
      firstName: 'Jake',
      lastName: 'Anderson',
      role: 'Manager',
      joined: '02/03/21'
    },
    {
      id: 3,
      firstName: 'Adam',
      lastName: 'Wallace',
      role: 'Worker',
      joined: '02/03/21'
    },
    {
      id: 4,
      firstName: 'Bob',
      lastName: 'Mac',
      role: 'Candidate',
      joined: '02/03/21'
    },
  ]
  changeWorkerRoleForm: FormGroup;

  constructor(private data: DataService, private fb: FormBuilder) {
    this.data.changeTitle(this.title);
    this.changeWorkerRoleForm = this.fb.group({
      role: ['', [Validators.required] ]
    });
  }

  ngOnInit(): void {}

  changeWorkerRole(i: number, form: any): void {
    this.workers.forEach(worker => {
      if (worker.id === i) {
        worker.role = form.role;
      }
    });
  }

}
