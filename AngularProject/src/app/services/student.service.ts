import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentModel } from '../models/student-model';
//import { StudentModel } from '../signUp/form/student-model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseURL: string = 'https://localhost:44351/api/Classes/';
  
  constructor(private http: HttpClient) { }


  getStudent(): Observable<StudentModel[]> {
    return this.http.get<StudentModel[]>(`${this.baseURL}GetStudentsList`);

  }
  save(st: StudentModel): Observable<any> {
    return this.http.post<any>(`${this.baseURL}SaveStudent`, st);
  }

  delete(st:StudentModel): Observable<any> {
    return this.http.delete<any>(`${this.baseURL}DeleteStudent/`+st.Id);
  }
  update(st: StudentModel): Observable<any> {
    return this.http.put<any>(`${this.baseURL}UpdateStudentDetails/`+st.Id, st);
  }
}
