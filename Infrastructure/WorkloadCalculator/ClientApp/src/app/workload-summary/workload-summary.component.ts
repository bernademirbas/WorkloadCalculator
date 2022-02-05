import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IWorkloadSummary } from "../models/workload-summary.model";
import { ICourse } from "../models/course.model";

@Component({
  selector: 'app-workload-summary',
  templateUrl: './workload-summary.component.html',
  styleUrls: ['./workload-summary.component.css']
})
export class WorkloadSummaryComponent implements OnInit {

  workloadSummary: IWorkloadSummary;
  weeklyHours = new Map<string, number>();
  startDate: Date;
  endDate: Date;
  totalHours: number;
  courses: ICourse[];

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const summary = navigation && navigation.extras && navigation.extras.state;
    if (summary) {
      this.workloadSummary = summary as IWorkloadSummary;
      this.endDate = new Date(this.workloadSummary.endDate);
      this.startDate = new Date(this.workloadSummary.startDate);
      this.courses = this.workloadSummary.courses;
      this.totalHours = this.workloadSummary.totalHours;

      var dayInterval = ((this.endDate).getTime() - (this.startDate).getTime()) / (1000 * 3600 * 24) + 1;
      let dailyAverageWorkHour = Math.floor(this.totalHours / dayInterval * 10) / 10;

      let assignedHours = 0;
      var date = new Date(this.startDate.valueOf());
      var endDate = new Date(this.endDate.valueOf());
      for (date; date <= endDate; date.setDate(date.getDate() + 1)) {
        let weekOfYear = this.getWeekNumber(date);
        let hoursOfWeek = this.weeklyHours.get(weekOfYear.toString());
        if (hoursOfWeek) {
          this.weeklyHours.set(weekOfYear.toString(), Math.round((hoursOfWeek + dailyAverageWorkHour) * 10) / 10);
        }
        else {
          this.weeklyHours.set(weekOfYear.toString(), dailyAverageWorkHour);
        }
        assignedHours += dailyAverageWorkHour;
        //add remaining hours to last week (for some decimal points when calculating daily work hour)
        if (date.toDateString() === this.endDate.toDateString()) {
          let remainingHours = Math.round((this.totalHours - assignedHours) * 10) / 10;
          this.weeklyHours.set(weekOfYear.toString(), this.weeklyHours.get(weekOfYear.toString()) + remainingHours);
        }
      }
    }
  }
  ngOnInit() {
  }


  private getWeekNumber(d: Date): number {
    d = new Date(+d);
    d.setHours(0, 0, 0);
    // Set to nearest Thursday: current date + 4 - current day number
    // Make Sunday's day number 7
    d.setDate(d.getDate() + 4 - (d.getDay() || 7));
    var yearStart = new Date(d.getFullYear(), 0, 1);
    // Calculate full weeks to nearest Thursday
    var weekNumber = Math.ceil((((d.valueOf() - yearStart.valueOf()) / 86400000) + 1) / 7);
    return weekNumber;
  }
}
