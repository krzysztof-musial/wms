import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.scss']
})
export class StorageComponent implements OnInit {

  title: string = 'Storage';

  constructor(private data: DataService) {
    this.data.changeTitle(this.title);
  }

  ngOnInit(): void {}

}
