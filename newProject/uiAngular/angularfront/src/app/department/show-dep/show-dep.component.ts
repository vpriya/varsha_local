import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Department } from 'src/app/model/department.model';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  constructor(private service:SharedService) { }

  DepartmentList: Department[] =[];

  ModalTitle!: string;
  ActivateAddEditDepComp:boolean=false;
  dep:any;

  ngOnInit(): void {
    this.refreshDepList();
  }

  addClick(){
    this.dep={
      departmentid:"0",
      departmentname:""
    }
    this.ModalTitle="Add Department";
    this.ActivateAddEditDepComp=true;
  }

  editClick(item: any){
    this.dep=item;
    this.ModalTitle="Edit Department";
    this.ActivateAddEditDepComp=true;
  }

  closeClick(){
    this.ActivateAddEditDepComp=false;
    this.refreshDepList();
  }

  refreshDepList(){
    this.service.getDepList().subscribe({

      next: (departmentList) => {
        this.DepartmentList = departmentList;
        console.log(departmentList);
      },
      error: (response) =>{
        console.log(response);
      }
    })

    this.DepartmentList.push();
   }
   

}
