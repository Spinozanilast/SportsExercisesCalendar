import { ExerciseCategory, ExerciseStatus } from './calendar.enums';

export interface Exercise {
  id: string;
  name: string;
  description: string;
  startDate: string;
  endDate: string;
  category: ExerciseCategory;
  status: ExerciseStatus;
  calendarDayId: string;
}
