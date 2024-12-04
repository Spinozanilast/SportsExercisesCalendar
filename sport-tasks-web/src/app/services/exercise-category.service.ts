import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ExerciseCategoryInfo } from '@data/exercise-category-info.model';
import { environment } from 'environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ExerciseCategoryService {
  private apiUrl = `${environment.apiUrl}/api/ExerciseCategories`;

  constructor(private http: HttpClient) {}

  getExerciseCategories(): Observable<ExerciseCategoryInfo[]> {
    return this.http.get<ExerciseCategoryInfo[]>(`${this.apiUrl}`);
  }
}
