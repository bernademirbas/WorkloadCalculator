import { ICourse } from "./course.model";

export interface IWorkloadSummary {
  startDate: Date;
  endDate: Date;
  totalHours: number;
  courses: ICourse[];
}
