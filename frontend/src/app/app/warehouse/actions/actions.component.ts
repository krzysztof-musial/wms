import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-actions',
  templateUrl: './actions.component.html',
  styleUrls: ['./actions.component.scss']
})
export class ActionsComponent implements OnInit {

  title: string = 'Actions';

  constructor(private data: DataService) {
    this.data.changeTitle(this.title);
  }

  ngOnInit(): void {}

}
