import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from './model/department.model';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly APIUrl = "https://localhost:44383/api";
  readonly PhotoUrl = "https://localhost:44383/PhotosEmp";

  constructor(private http:HttpClient) { }

  getDepList():Observable<Department[]>{
    return this.http.get<Department[]>(this.APIUrl+'/department');
  }

  addDepartment(val:any){
    return this.http.post(this.APIUrl+'/Department',val);
  }

  updateDepartment(val:any): Observable<Object>{
    return this.http.put(this.APIUrl+'/Department',val);
  }

  deleteDepartment(val:any){
    return this.http.delete(this.APIUrl+'/Department/'+val);
  }

  getEmpList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Employee');
  }

  addEmployee(val:any){
    return this.http.post(this.APIUrl+'/Employee',val);
  }

  updateEmployee(val:any){
    return this.http.put(this.APIUrl+'/Employee',val);
  }

  deleteEmployee(val:any){
    return this.http.delete(this.APIUrl+'/Employee/'+val);
  }

  UploadPhoto(val:any){
    return this.http.post(this.APIUrl+'/Employee/SaveFile',val);
  }

  getAllDepartmentNames():Observable<any[]>{
    return this.http.get<any[]>(this.APIUrl+'/Employee/GetAllDepartmentNames');
  }

}
