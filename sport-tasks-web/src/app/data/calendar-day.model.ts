import { Exercise } from './exercise.model';

export interface CalendarDay {
  id: string;
  date: Date;
  calendarId: string;
  sportTasks: Exercise[];
}
