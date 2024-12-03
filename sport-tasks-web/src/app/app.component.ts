import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DatepickerComponent } from '@components/datepicker/datepicker.component';
import { CalendarsArrayComponent } from './components/calendars-array/calendars-array.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CalendarsArrayComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'sport-tasks-web';
}
