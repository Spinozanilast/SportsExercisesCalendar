// exercise-dialog.component.ts
import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  inject,
  Inject,
  Output,
} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ExerciseStatusValues } from '@data/calendar.enums';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { ExerciseCategoryService } from '@services/exercise-category.service';
import { ExerciseService } from '@services/exercise.service';
import { ExerciseCategoryInfo } from '@data/exercise-category-info.model';
import { NgxMatTimepickerModule } from 'ngx-mat-timepicker';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { Exercise, ExerciseRequest } from '@data/exercise.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  standalone: true,
  selector: 'app-exercise-dialog',
  templateUrl: './exercise-create-dialog.component.html',
  styleUrls: ['./exercise-create-dialog.component.css'],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatDialogContent,
    MatDialogActions,
    NgxMatTimepickerModule,
    MatIcon,
    MatButton,
    MatDialogModule,
  ],
  providers: [provideNativeDateAdapter()],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ExerciseDialogComponent {
  private readonly exerciseCategoryService = inject(ExerciseCategoryService);
  private readonly exerciseService = inject(ExerciseService);
  private readonly calendarDayId: string = this.data;
  private readonly snackBar = inject(MatSnackBar);

  exerciseForm: FormGroup;
  categories: ExerciseCategoryInfo[] = [];
  statusValues = Object.values(ExerciseStatusValues);

  @Output() exerciseCreated = new EventEmitter<Exercise>();

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ExerciseDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.initCategories();

    this.exerciseForm = this.fb.group({
      name: ['', Validators.required],
      goal: [0, Validators.required],
      description: [''],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      category: [this.categories[0], Validators.required],
      status: [ExerciseStatusValues.NotStarted, Validators.required],
    });
  }

  private initCategories() {
    this.exerciseCategoryService.getExerciseCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (error) => {
        console.error('Error fetching exercise categories', error);
      },
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.exerciseForm.valid) {
      console.log(this.exerciseForm.value);

      const exerciseRequest = this.createExerciseRequest();
      console.log(exerciseRequest);
      this.exerciseService.addExercise(exerciseRequest).subscribe({
        next: (exercise) => {
          this.dialogRef.close(exercise);
        },
        error: (error) => {
          this.openSnackBar('Error creating exercise', 'Close');
        },
      });
    }
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action);
  }

  private createExerciseRequest(): ExerciseRequest {
    return {
      name: this.exerciseForm.value.name,
      goal: this.exerciseForm.value.goal,
      description: this.exerciseForm.value.description,
      startDate: this.exerciseForm.value.startDate,
      endDate: this.exerciseForm.value.endDate,
      category: this.exerciseForm.value.category,
      status: this.exerciseForm.value.status,
      calendarDayId: this.calendarDayId,
    };
  }
}
