import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-icon',
  templateUrl: './icon.component.html',
  styleUrls: ['./icon.component.scss']
})
export class IconComponent implements OnInit {

  @Input() icon: string;
  @Input() colorPrimary: string; // 'black' or 'white'
  @Input() colorSecondary: string; // '#999999'

  constructor() { }

  ngOnInit(): void {
  }

}
