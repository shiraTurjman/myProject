import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Classes } from 'src/app/models/classes';
import { StudentModel } from 'src/app/models/student-model';
import { ClassesService } from 'src/app/services/classes.service';
import { StudentService } from 'src/app/services/student.service';
import {map, startWith } from 'rxjs/operators';




@Component({
  selector: 'app-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['./sign-up-form.component.scss']
})
export class SignUpFormComponent implements OnInit {
  lf!: FormGroup;
  st1: StudentModel = {  'Id': 0,'Name': "", 'Phone': "",  'Address': "",'Email': "", 'Age': 0, 'ClassCode': 0, 'MonthNumber': 0, ClassName: "" };
  @Input() 
  st!: StudentModel
  @Input()
  flag:boolean=true;
  @Output()
  onUpdate=new EventEmitter();
//sss!:StudentModel;


  myControl = new FormControl();
  options: Classes[] = [];
  filteredOptions!: Observable<Classes[]>;

  

  myClasses: Classes[] = [];
  constructor(private cs: ClassesService, private ss: StudentService, private route: Router) { }

  ngOnInit(): void {
    //this.st = this.ss.student;
    this.cs.getClass().subscribe(res => this.myClasses = res);
    this.cs.getClass().subscribe(res => this.options = res);
    //this.myClasses=this.cs.getClass().subscribe(res=>this.myClasses=res);
    if(this.flag==true){
    this.lf = new FormGroup({
      id: new FormControl("", [Validators.required, Validators.minLength(9), Validators.maxLength(9),Validators.min(1), Validators.pattern(/^[0-9]\d*$/)]),
      name: new FormControl("",[Validators.required, Validators.minLength(2)]),
      phone: new FormControl("", [Validators.required, Validators.minLength(10), Validators.maxLength(10), Validators.pattern(/^[0-9]\d*$/)]),
      address: new FormControl("", [Validators.required, Validators.minLength(4)]),
      Email: new FormControl("", [Validators.required, Validators.email]),
      age: new FormControl("", [Validators.required,Validators.pattern(/^[0-9]\d*$/), Validators.min(4), Validators.max(20)]),
      ClassCode: new FormControl("",[Validators.required]),
      MonthNumber: new FormControl("",[Validators.required]),
      Price: new FormControl("")
    });}
    else{
      this.lf = new FormGroup({
        id: new FormControl(this.st.Id, [Validators.required, Validators.minLength(9), Validators.maxLength(9),Validators.min(1), Validators.pattern(/^[0-9]\d*$/)]),
        name: new FormControl(this.st.Name,[Validators.required, Validators.minLength(2)]),
        phone: new FormControl(this.st.Phone, [Validators.required, Validators.minLength(10), Validators.maxLength(10), Validators.pattern(/^[0-9]\d*$/)]),
        address: new FormControl(this.st.Address, [Validators.required, Validators.minLength(4)]),
        Email: new FormControl(this.st.Email, [Validators.required, Validators.email]),
        age: new FormControl(this.st.Age, [Validators.required,Validators.pattern(/^[0-9]\d*$/), Validators.min(4), Validators.max(20)]),
        ClassCode: new FormControl(this.st.ClassCode,[Validators.required]),
        MonthNumber: new FormControl(this.st.MonthNumber,[Validators.required]),
        Price: new FormControl(this.st.Price)
      });
      this.lf.get('id')?.disable();

    }

    this.lf.get('Price')?.disable();

  // this.lf.setValue(this.st);

     
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
  
    const filterValue = name.toLowerCase();

    return this.options.filter(option => option.Name.toLowerCase().includes(filterValue));
  }


    
  
  getPrice() {
    // {debugger;
    let x = 0;
    let p = 0;
    for (let index = 0; index < this.myClasses.length; index++) {


      if (this.myClasses[index].Code == this.lf.get('ClassCode')?.value) {
        x = this.myClasses[index].Price;
        break;
      }
    } console.log(x);

    let y = this.lf.get('MonthNumber')?.value;

    // switch(y)
    // {//case null:p=null; break;
    //   case 1:p=x; break;
    //   case 3:p=x*3*0.95; break;
    //   case 6:p=x*6*0.85; break;
    //   case 12:p=x*12*0.8; break;
    // }
    if (y == 1)
      p = x;
    else
      if (y == 3)
        p = x * 3 * 0.95;
      else
        if (y == 6)
          p = x * 6 * 0.85;
        else
          if (y == 12)
            p = x * 12 * 0.8;
    console.log(p);


    this.lf.get('Price')?.setValue(p);

  }

  save(): void {

    this.st1.Id = parseInt(this.lf.get('id')?.value);
    this.st1.Name = this.lf.get('name')?.value;
    this.st1.Phone = this.lf.get('phone')?.value;
    this.st1.Email = this.lf.get('Email')?.value;

    this.st1.Address = this.lf.get('address')?.value;
    this.st1.Age = parseInt(this.lf.get('age')?.value);
    this.st1.ClassCode = parseInt(this.lf.get('ClassCode')?.value);
    this.st1.MonthNumber = parseInt(this.lf.get('MonthNumber')?.value);
      
    this.st1.Price = parseInt(this.lf.get('Price')?.value);
    debugger;    console.log(this.st);
    //שמירה
    if (this.flag == true) {
      this.ss.save(this.st1).subscribe(res => {  alert("נרשמת בהצלחה ! "); this.route.navigateByUrl("Classes/managementStudent"); }, err => { alert("שגיאה!!!!") })
    }
    //עדכון
    else {
      debugger;
      this.ss.update(this.st1).subscribe(res => {  alert("השינוי נעשה  בהצלחה  !"); this.flag = true; this.onUpdate.emit();  }, err => { alert("שגיאה!!!!") })
      
    }

  }





}