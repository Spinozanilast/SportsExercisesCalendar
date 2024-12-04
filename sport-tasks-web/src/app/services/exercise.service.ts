import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry, delay } from 'rxjs/operators';
import { environment } from 'environments/environment.development';
import { Exercise, ExerciseRequest } from '@data/exercise.model';

@Injectable({
  providedIn: 'root',
})
export class ExerciseService {
  private apiUrl = `${environment.apiUrl}/api/Exercise`;

  constructor(private http: HttpClient) {}

  getExerciseById(id: string): Observable<Exercise> {
    return this.http
      .get<Exercise>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  getAllExercisesByCalendarDayId(
    calendarDayId: string
  ): Observable<Exercise[]> {
    return this.http
      .get<Exercise[]>(`${this.apiUrl}/by-calendar-day/${calendarDayId}`)
      .pipe(catchError(this.handleError));
  }

  addExercise(exerciseRequest: ExerciseRequest): Observable<Exercise> {
    return this.http
      .post<Exercise>(this.apiUrl, exerciseRequest, this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  updateExercise(id: string, exercise: Exercise): Observable<void> {
    return this.http
      .put<void>(`${this.apiUrl}/${id}`, exercise, this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  deleteExercise(id: string): Observable<void> {
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
