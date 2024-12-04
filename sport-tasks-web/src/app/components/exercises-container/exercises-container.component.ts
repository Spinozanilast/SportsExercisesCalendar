import { Component, Input } from '@angular/core';
import { Exercise } from '@data/exercise.model';

import { ExerciseCardComponent } from '../exercise-card/exercise-card.component';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { ExerciseDialogComponent } from '../dialogs/exercise-create-dialog/exercise-create-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-exercises-container',
  standalone: true,
  imports: [ExerciseCardComponent, MatIcon, MatButton],
  templateUrl: './exercises-container.component.html',
  styleUrl: './exercises-container.component.css',
})
export class ExercisesContainerComponent {
  @Input() exercises!: Exercise[];
  @Input() date!: Date;

  constructor(private exerciseCreateDialog: MatDialog) {}

  openExerciseCreationDialog() {
    const dialogRef = this.exerciseCreateDialog.open(ExerciseDialogComponent, {
      data: this.exercises[0].calendarDayId,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        console.log('Exercise Created:', result);
      }
    });
  }
}
