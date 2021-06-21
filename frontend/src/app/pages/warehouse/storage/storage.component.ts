import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.scss']
})
export class StorageComponent implements OnInit {

  articles;

  constructor(private fb: FormBuilder, private ss: StorageService) {
    this.ss.getAllArticles().subscribe((articles) => {
      this.articles = articles;
      console.log(articles)
    })
  }

  ngOnInit(): void {
  }

}
