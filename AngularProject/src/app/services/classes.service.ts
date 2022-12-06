import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Classes } from '../models/classes';

@Injectable({
  providedIn: 'root'
})
export class ClassesService {
 
  options: string[] = ['One', 'Two', 'Three'];
  baseURL:string='https://localhost:44351/api/Classes/';
  constructor(private http:HttpClient) { }
     
  getClass():Observable<Classes[]>
  {
    return this.http.get<Classes[]> (`${this.baseURL}GetClassesList`);
  
  }
 

}
