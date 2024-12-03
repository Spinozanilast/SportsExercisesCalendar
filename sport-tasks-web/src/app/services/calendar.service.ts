import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, delay, retry } from 'rxjs/operators';
import { environment } from 'environments/environment.development';
import { Calendar, CalendarRequest } from '@data/calendar.models';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  private apiUrl = `${environment.apiUrl}/api/Calendar`;

  constructor(private http: HttpClient) {}

  getCalendarById(id: string): Observable<Calendar> {
    return this.http
      .get<Calendar>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  getAllCalendars(): Observable<Calendar[]> {
    return this.http
      .get<Calendar[]>(this.apiUrl)
      .pipe(retry({ count: 3, delay: 1000 }), catchError(this.handleError));
  }

  addCalendar(calendarRequest: CalendarRequest): Observable<Calendar> {
    return this.http
      .post<Calendar>(this.apiUrl, calendarRequest, this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  updateCalendar(id: string, calendar: Calendar): Observable<void> {
    return this.http
      .put<void>(`${this.apiUrl}/${id}`, calendar, this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  deleteCalendar(id: string): Observable<void> {
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
