import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignUpFormComponent } from './sign-up-form/sign-up-form.component';
import { ManagementStudentListComponent } from './management-student-list/management-student-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { BpriceDirective } from '../directive/bprice.directive';
import { HlDirective } from './hl.directive';
import { HomePaqeComponent } from './home-paqe/home-paqe.component';
import { PaqeNotFoundComponent } from './paqe-not-found/paqe-not-found.component';
import {HttpClientModule} from '@angular/common/http';
//import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


const classesRouts: Routes = [
  { path:'managementStudent',component:ManagementStudentListComponent},
  {path:'signUp',component:SignUpFormComponent},
  {path:'',component:HomePaqeComponent},
  {path:'**',component:PaqeNotFoundComponent}
  ];

@NgModule({
  declarations: [
    SignUpFormComponent,
    ManagementStudentListComponent,
    HlDirective,
    HomePaqeComponent,
    PaqeNotFoundComponent,  
   //  BpriceDirective,

  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(classesRouts),
    MatAutocompleteModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    HttpClientModule,
    //BrowserAnimationsModule
    //BrowserModule,

    
    
    
    
  ],
  exports:[  SignUpFormComponent,
    ManagementStudentListComponent]
 
})

export class ClassesModule {  
}
