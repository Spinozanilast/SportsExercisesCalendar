import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'environments/environment.development';
import { CalendarDay } from '@data/calendar-day.model';

@Injectable({
  providedIn: 'root',
})
export class CalendarDayService {
  private apiUrl = `${environment.apiUrl}/api/CalendarDay`;

  constructor(private http: HttpClient) {}

  getCalendarDayById(id: string): Observable<CalendarDay> {
    return this.http
      .get<CalendarDay>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  getCalendarDaysByCalendarId(calendarId: string): Observable<CalendarDay[]> {
    return this.http
      .get<CalendarDay[]>(`${this.apiUrl}/by-calendar/${calendarId}`)
      .pipe(catchError(this.handleError));
  }

  addCalendarDay(calendarDay: CalendarDay): Observable<CalendarDay> {
    return this.http
      .post<CalendarDay>(this.apiUrl, calendarDay, this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  updateCalendarDay(id: string, calendarDay: CalendarDay): Observable<void> {
    return this.http
      .put<void>(`${this.apiUrl}/${id}`, calendarDay, this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  deleteCalendarDay(id: string): Observable<void> {
    return this.http
      .delete<void>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(error);
  }
}
