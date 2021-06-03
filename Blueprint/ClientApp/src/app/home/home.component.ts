import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Employee } from './employee';
import { Position } from './position';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.css']
})
export class HomeComponent implements OnInit {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<Array<Employee>>(baseUrl + "api/employees").subscribe(result => {
      this.employees = result;
      this.selectedEmployee = result[0].id;
      this.employee = result[0];
      this.onSelectedEmployeeChange();
      
    })

    http.get<Array<Position>>(baseUrl + "api/employeepositions").subscribe(result => {
      this.positions = result;
    })
    
  }

  onSelectedEmployeeChange() {
    this.fullName = this.employee.fullName;
    this.address = this.employee.address;
    this.phoneNumber = this.employee.phoneNumber;
    this.position = this.employee.position.id;
  }


  ngOnInit(): void {
      
  }

  fullName: string;
  address: string;
  phoneNumber: string;
  position: number;


  employees: Array<Employee> = [];
  selectedEmployee: number = 1;
  employee: Employee;

  positions: Array<Position> = [];

  handleEmployeeCardClick(id: number) {
    this.selectedEmployee = id;
    this.employee = this.employees.filter(it => it.id == id)[0];
    this.onSelectedEmployeeChange();
  }

  handleSaveClick() {

    if (this.selectedEmployee == -1) {
      this.http.post<Array<Employee>>(this.baseUrl + 'api/employees', {
        fullName: this.fullName,
        address: this.address,
        phoneNumber: this.phoneNumber,
        position: this.position
      }).subscribe(result => {
        this.employees = result;
        this.selectedEmployee = result[0].id;
        this.employee = result[0];
        this.onSelectedEmployeeChange();
      })
    } else {
      this.http.put<Array<Employee>>(this.baseUrl + "api/employees/" + this.selectedEmployee, {
        id: this.selectedEmployee,
        fullName: this.fullName,
        address: this.address,
        phoneNumber: this.phoneNumber,
        position: this.position
      }).subscribe(result => {
        this.employee.fullName = this.fullName;
        this.employee.address = this.address;
        this.employee.phoneNumber = this.phoneNumber;
        this.employee.position = this.positions.filter(it => it.id == this.position)[0];
      })
    }

  }

  handleAddNewClick() {
    if (this.selectedEmployee == -1) return;
    this.selectedEmployee = -1;
    
    let newEmployee: Employee = {
      id: -1,
      fullName: 'New Employee*',
      address: '',
      phoneNumber: '',
      position: this.positions[0]
    };
    this.employees.unshift(newEmployee);
    this.employee = newEmployee;
    this.onSelectedEmployeeChange();
  }

  handleDeleteClick(id: number) {
    this.http.delete(this.baseUrl + "api/employees/" + id).subscribe(result => {
      this.employees = this.employees.filter(it => it.id !== id);
      this.selectedEmployee = this.employees[0].id;
      this.employee = this.employees[0];
      this.onSelectedEmployeeChange();
    })
  }
}
