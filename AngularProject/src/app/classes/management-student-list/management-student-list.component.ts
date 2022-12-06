import { Component, OnInit } from '@angular/core';

import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { Classes } from 'src/app/models/classes';
import { StudentModel } from 'src/app/models/student-model';
import { ClassesService } from 'src/app/services/classes.service';
import { StudentService } from 'src/app/services/student.service';
import {map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-management-student-list',
  templateUrl: './management-student-list.component.html',
  styleUrls: ['./management-student-list.component.scss']
})
export class ManagementStudentListComponent implements OnInit {

  myStudentList: StudentModel[] = []
 sortStudent: StudentModel[] =[] ;
 myClasses: Classes[] = [];
 selectedValue=0;

 myControl = new FormControl();
 options: Classes[] = [];
 filteredOptions!: Observable<Classes[]>;
 
 allClass:Classes={'Name':"כל החוגים",'Code':0,'Price':0};
 //-------
//  myControl = new FormControl();
//  options: string[] = ['One', 'Two', 'Three'];
//  filteredOptions!: Observable<string[]>;

 showd: boolean = false;
 selectedI!: StudentModel;
 
 //------
  constructor(private cs: ClassesService, private ss: StudentService, private route: Router) {
    
   }

  
  ngOnInit(): void {
    debugger;
   // this.ss.student = { 'Name': "", 'Id': 0, 'Phone': "", 'Email': "", 'Address': "", 'Age': 0, 'ClassCode': 0, 'MonthNumber': 0, ClassName: "" };;
   this.showd=false;
    this.ss.getStudent().subscribe(res => this.myStudentList = res);
    //this.sortStudent = this.myStudentList;
    this.ss.getStudent().subscribe(res => this.sortStudent = res);

    this.cs.getClass().subscribe(res => this.myClasses = res);
    this.cs.getClass().subscribe(res => this.options = res);
    // selectedValue='0';
    //-----------
    // this.filteredOptions = this.myControl.valueChanges.pipe(
    //   startWith(''),
    //   map(value => this._filter(value))
  //  );
    //-------
    //this.filteredOptions.subscribe(res=>res=this.options)

   
    this.filteredOptions = this.myControl.valueChanges.pipe(
     
      startWith(''),
      map(value => (typeof value === 'string' ? value : value.name)),
      map(name => (name ? this._filter(name) : this.options.slice())),
    );
   
  }

  displayFn(user: Classes): string {
    debugger;
    return user && user.Name ? user.Name : '';
  }

  private _filter(name: string): Classes[] {
    debugger;
    const filterValue = name.toLowerCase();

    return this.options.filter(option => option.Name.toLowerCase().includes(filterValue));
  }

  update(student:StudentModel) {
     this.selectedI=student;
     this.showd=true;

    //this.selectedI=st1;
    //this.ss.student = st1;
    //this.ss.flag = false;
    //this.route.navigateByUrl("Classes/signUp");

  }
  updete2():void{
    
    debugger;
    this.ngOnInit();
  }
  delete(st: StudentModel) {
    debugger;
    this.ss.delete(st).subscribe(res => { alert("נמחק בהצלחה !"); this.ngOnInit(); }, err => { alert("שגיאה!!!!") });
  }
 
  sortByClass(vc:Classes) {
    debugger;
    let value=vc.Code;
    //value=value.Code;
    if (value != 0&&value!=undefined) {
      this.sortStudent = [];
      for (let index = 0; index < this.myStudentList.length; index++) {
        if (this.myStudentList[index].ClassCode == value)
        this.sortStudent.push(this.myStudentList[index]);
    }
    

    }else
    {this.sortStudent=this.myStudentList;}
    //this.route.navigateByUrl("Classes/managementStudent");
    //this.ngOnInit();
  }
  
  //----------
  // private _filter(value: Classes): string[] {
  //   const filterValue = value.toLowerCase();
 
  //   return this.options.filter(option => option.toLowerCase().includes(filterValue));
  // }
  //-------
  
}