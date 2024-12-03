import { Component, inject } from '@angular/core';
import { DatepickerComponent } from '@components/datepicker/datepicker.component';
import { Calendar } from '@data/calendar.models';
import { CalendarService } from '@services/calendar.service';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButton } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ExercisesContainerComponent } from '@components/exercises-container/exercises-container.component';
import { MatDividerModule } from '@angular/material/divider';
import { CalendarDay } from '@data/calendar-day.model';

@Component({
  selector: 'app-calendars-array',
  standalone: true,
  imports: [
    DatepickerComponent,
    MatTabsModule,
    MatButton,
    MatIconModule,
    ExercisesContainerComponent,
    MatDividerModule,
  ],
  templateUrl: './calendars-array.component.html',
  styleUrl: './calendars-array.component.css',
})
export class CalendarsArrayComponent {
  private readonly calendarService = inject(CalendarService);

  selectedCalendarDay: CalendarDay | null = null;
  calendars: Calendar[] = [];
  isLoading: boolean = true;
  selectedIndex: number = 0;

  constructor() {
    this.calendarService.getAllCalendars().subscribe(
      (calendars) => {
        this.calendars = calendars;
        this.isLoading = false;
      },
      (error) => {
        this.isLoading = true;
      }
    );
  }

  setActiveExercisesIndex(index: number): void {
    this.selectedIndex = index;
  }

  selectCalendarDay(calendarDay: CalendarDay): void {
    console.log(calendarDay);
    this.selectedCalendarDay = calendarDay;
  }
}
