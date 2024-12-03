import { CalendarDay } from './calendar-day.model';
import { ExerciseCategory, ExerciseStatus } from './calendar.enums';

export interface Calendar {
  id: string;
  name: string;
  startDate: Date;
  endDate: Date;
  calendarDays: CalendarDay[];
}

export interface CalendarRequest {
  name: string;
  startDate: Date;
  endDate: Date;
  calendarDays: CalendarDay[];
}
