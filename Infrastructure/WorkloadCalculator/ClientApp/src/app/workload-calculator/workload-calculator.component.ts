import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ICourse } from '../models/course.model';
import { IWorkloadSummary } from "../models/workload-summary.model";

@Component({
  selector: 'app-workload-calculator',
  templateUrl: './workload-calculator.component.html',
  styleUrls: ['./workload-calculator.component.css']
})
export class WorkloadCalculatorComponent implements OnInit {
  public coursesForm: FormGroup;
  errors: string[];
  baseUrl: string;
  courseList: ICourse[];
  minimumDate: Date = new Date();
  maximumDate: Date = new Date();

  constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.minimumDate.setDate(this.minimumDate.getDate() + 1);
    this.maximumDate.setDate(this.minimumDate.getDate() + 365);
    this.baseUrl = baseUrl;
    this.createCourseForm();
  }

  ngOnInit() {
    this.loadCourses();
  }

  loadCourses() {
    this.http.get<ICourse[]>(this.baseUrl + 'Courses').subscribe(result => {
      result.forEach((item) => {
        item.status = false;
      });
      this.courseList = result;
      var endDate = new Date();
      endDate.setDate(endDate.getDate() + 7);
      var startDate = new Date();
      startDate.setDate(startDate.getDate() + 1);
      this.coursesForm = this.fb.group({
        courses: this.fb.array(
          this.courseList.map(animal => {
            return this.fb.group({
              id: animal.id,
              name: animal.name,
              totalHour: animal.totalHour,
              status: animal.status
            });
          })
        ),
        startDate: new FormControl(formatDate(startDate, 'yyyy-MM-dd', 'en-US')),
        endDate: new FormControl(formatDate(endDate, 'yyyy-MM-dd', 'en-US'))
      });
    });
  }

  createCourseForm() {
    this.coursesForm = new FormGroup({
      startDate: new FormControl('', [Validators.required]),
      endDate: new FormControl('', Validators.required),
      courses: new FormArray([], Validators.required)
    });
  }

  onSubmit() {
    const formValue = this.coursesForm.value;
    formValue.courses = formValue.courses.filter(c => c.status);
    if (formValue.courses.length <= 0) {
      this.errors = new Array<string>("Please select course!");
      return;
    }
    this.http.post<IWorkloadSummary>(this.baseUrl + 'workload/generate', formValue).subscribe(result => {
      let apiResult: any = result;
      if (apiResult.StatusCode === 500) {
        this.errors = new Array<string>(apiResult.Message);
      } else {
        result.courses = formValue.courses;
        const navigationExtras: NavigationExtras = { state: result };
        this.router.navigate(['/workload-summary'], navigationExtras);
      }
    }
    );
  }
}
