import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
//import { FormSignUpComponent } from './signUp/form/form-sign-up/form-sign-up.component';
import {ReactiveFormsModule} from '@angular/forms';
//import { DetailsComponent } from './signUp/details/details/details.component';
import {HttpClientModule} from '@angular/common/http';
import { ClassesModule } from './classes/classes.module';
import { RouterModule, Routes } from '@angular/router';
//import { BpriceDirective } from './directive/bprice.directive';
import { UnlessDirective } from './directive/unless.directive';

//import { SignUpModule } from './signUp/classes-module/sign-up.module';

// const routes: Routes = [
//   {path:"Classes",loadChildren:()=>import("./classes/classes.module").then(m=>m.ClassesModule)
// }
// ];

@NgModule({
  declarations: [
    AppComponent,
    UnlessDirective
      
    
    
  ],
  imports: [
     BrowserModule,
    AppRoutingModule,ReactiveFormsModule,HttpClientModule,
    ClassesModule,RouterModule
  
  ],
  exports:[RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
// function routes(routes: any): any[] | import("@angular/core").Type<any> | import("@angular/core").ModuleWithProviders<{}> {
//   throw new Error('Function not implemented.');
// }
