import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

  title: string = 'Settings';

  constructor(private data: DataService) {
    this.data.changeTitle(this.title);
  }

  ngOnInit(): void {}

}
