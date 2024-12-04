export interface Exercise {
  id: string;
  name: string;
  goal: number;
  description: string;
  startDate: string;
  endDate: string;
  category: number;
  status: number;
  calendarDayId: string;
}

export interface ExerciseRequest {
  name: string;
  goal: number;
  description: string;
  startDate: string;
  endDate: string;
  category: number;
  status: number;
  calendarDayId: string;
}
