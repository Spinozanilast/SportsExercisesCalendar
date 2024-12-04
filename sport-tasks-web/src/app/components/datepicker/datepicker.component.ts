import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  model,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { DayPickerComponent } from '@components/day-picker/day-picker.component';
import { CalendarDay } from '@data/calendar-day.model';
import { Calendar } from '@data/calendar.models';

const today = new Date();
const month = today.getMonth();
const year = today.getFullYear();
const day = today.getDay();

@Component({
  standalone: true,
  selector: 'app-datepicker',
  imports: [
    MatFormFieldModule,
    MatDatepickerModule,
    FormsModule,
    ReactiveFormsModule,
    DayPickerComponent,
  ],
  templateUrl: './datepicker.component.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './datepicker.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DatepickerComponent implements OnInit {
  @Input() calendar!: Calendar;
  @Output() calendarDaySelected = new EventEmitter<CalendarDay>();

  readonly calendarBoundaries = new FormGroup({
    start: new FormControl(new Date(year, month, day)),
    end: new FormControl(new Date(year, month, day)),
  });

  selected = model<CalendarDay | null>(null);

  ngOnInit(): void {
    if (this.calendar) {
      this.calendarBoundaries.patchValue({
        start: this.calendar.startDate,
        end: this.calendar.endDate,
      });
    }
  }

  onCalendarDaySelected(day: CalendarDay): void {
    this.calendarDaySelected.emit(day);
  }
}
