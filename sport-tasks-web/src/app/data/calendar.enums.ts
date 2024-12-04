export enum ExerciseStatusValues {
  NotStarted = 0,
  InProgress = 1,
  Completed = 2,
}

export type ExerciseStatus =
  (typeof ExerciseStatusValues)[keyof typeof ExerciseStatusValues];
