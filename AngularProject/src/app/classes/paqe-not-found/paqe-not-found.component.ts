import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-paqe-not-found',
  templateUrl: './paqe-not-found.component.html',
  styleUrls: ['./paqe-not-found.component.scss']
})
export class PaqeNotFoundComponent implements OnInit {

  constructor(private route:Router) { }

  ngOnInit(): void {
  }

  studentlist():void
  {
    this.route.navigateByUrl("Classes/managementStudent");

  }
  signup():void{
    this.route.navigateByUrl("Classes/signUp");
  }
}
