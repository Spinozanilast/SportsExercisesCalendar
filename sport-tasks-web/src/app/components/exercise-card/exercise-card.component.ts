import { Component, inject, Inject, Input, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Exercise } from '@data/exercise.model';
import { MatCardModule } from '@angular/material/card';
import { MatButton } from '@angular/material/button';
import { MatDivider } from '@angular/material/divider';
import { MatMenuModule } from '@angular/material/menu';
import { ExerciseStatusValues } from '@data/calendar.enums';
import { ExerciseCategoryService } from '@services/exercise-category.service';
import { ExerciseCategoryInfo } from '@data/exercise-category-info.model';

@Component({
  selector: 'app-exercise-card',
  standalone: true,
  imports: [MatIcon, MatCardModule, MatButton, MatDivider, MatMenuModule],
  templateUrl: './exercise-card.component.html',
  styleUrl: './exercise-card.component.css',
})
export class ExerciseCardComponent implements OnInit {
  private readonly exerciseCategoryInfo = inject(ExerciseCategoryService);
  @Input() exercise!: Exercise;
  statusEnum = ExerciseStatusValues;
  isEditing: boolean = false;
  category?: ExerciseCategoryInfo;

  ngOnInit(): void {
    this.exerciseCategoryInfo.getExerciseCategories().subscribe({
      next: (categories) => {
        this.category = categories.find(
          (category) => category.category === this.exercise.category
        );
      },
    });
  }

  toggleEditing(): void {
    this.isEditing = !this.isEditing;
  }

  setExerciseStatus(status: ExerciseStatusValues): void {
    this.exercise.status = status;
  }
}
