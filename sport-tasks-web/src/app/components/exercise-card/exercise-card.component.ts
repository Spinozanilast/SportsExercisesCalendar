import { Component, Input } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Exercise } from '@data/exercise.model';
import { MatCardModule } from '@angular/material/card';
import { MatButton } from '@angular/material/button';
import { MatDivider } from '@angular/material/divider';
import { MatMenuModule } from '@angular/material/menu';
import { ExerciseStatus } from '@data/calendar.enums';

@Component({
  selector: 'app-exercise-card',
  standalone: true,
  imports: [MatIcon, MatCardModule, MatButton, MatDivider, MatMenuModule],
  templateUrl: './exercise-card.component.html',
  styleUrl: './exercise-card.component.css',
})
export class ExerciseCardComponent {
  @Input() exercise!: Exercise;
  statusEnum = ExerciseStatus;
  isEditing: boolean = false;

  toggleEditing(): void {
    this.isEditing = !this.isEditing;
  }

  startExercise(): void {
    console.log('start');
    this.exercise.status = ExerciseStatus.InProgress;
  }
}
