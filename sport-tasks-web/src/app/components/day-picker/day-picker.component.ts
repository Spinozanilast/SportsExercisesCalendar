import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { provideNativeDateAdapter } from '@angular/material/core';
import {
  MatCalendarCellClassFunction,
  MatDatepicker,
  MatDatepickerInputEvent,
  MatDatepickerModule,
} from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Calendar } from '../../data/calendar.models';
import { CalendarDay } from '@data/calendar-day.model';
import { MatButtonModule } from '@angular/material/button';

/** @title Datepicker with filter validation */
@Component({
  standalone: true,
  styleUrl: 'day-picker.component.css',
  selector: 'app-day-picker',
  templateUrl: 'day-picker.component.html',
  encapsulation: ViewEncapsulation.None,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatButtonModule,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DayPickerComponent {
  @Input() startDate!: Date;
  @Input() endDate!: Date;
  @Input() calendarDays!: CalendarDay[];
  @Output() selectedCalendarDay = new EventEmitter<CalendarDay>();

  @ViewChild('picker') picker!: MatDatepicker<Date>;

  myFilter = (d: Date | null): boolean => {
    if (!d) {
      return true;
    }
    const formattedDate = this.formatDate(d);
    return (
      formattedDate >= this.startDate.toString() &&
      formattedDate <= this.endDate.toString()
    );
  };

  private formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  dateClass: MatCalendarCellClassFunction<Date> = (cellDate, _) => {
    const date = this.formatDate(cellDate);
    return this.calendarDays.some((day) => {
      return day.date.toString() === date;
    })
      ? 'highlighted-date'
      : '';
  };

  setActiveDate(year: number, month: number): void {
    const newDate = new Date(year, month - 1);
    this.picker.select(newDate);
  }

  dateChanged(event: MatDatepickerInputEvent<Date>): void {
    if (event.value) {
      const selectedDay = this.formatDate(event.value!);
      const day: CalendarDay | undefined = this.calendarDays.find((day) => {
        return day.date.toString() === selectedDay;
      });
      if (day) {
        this.selectedCalendarDay.emit(day);
      } else {
        // TODO: You can redo controller logic in back to insert new day
        // for full here (better to do client interaction with some safe button)
      }
    }
  }
}
