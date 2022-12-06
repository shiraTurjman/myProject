import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-paqe',
  templateUrl: './home-paqe.component.html',
  styleUrls: ['./home-paqe.component.scss']
})
export class HomePaqeComponent implements OnInit {

  constructor(private route: Router) { }

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
