import { Component, Input } from '@angular/core';
import { Exercise } from '@data/exercise.model';
import { ExerciseCardComponent } from '../exercise-card/exercise-card.component';

@Component({
  selector: 'app-exercises-container',
  standalone: true,
  imports: [ExerciseCardComponent],
  templateUrl: './exercises-container.component.html',
  styleUrl: './exercises-container.component.css',
})
export class ExercisesContainerComponent {
  @Input() exercises!: Exercise[];
  @Input() date!: Date;
}
