import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {path:"Classes",loadChildren:()=>import("./classes/classes.module").then(m=>m.ClassesModule),
  
}
];

@NgModule({
   imports: [RouterModule.forRoot(routes)],
   
  exports: [RouterModule]
})
export class AppRoutingModule { }
