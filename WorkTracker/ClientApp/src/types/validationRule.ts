export interface ValidationRule<T> {
  validate: (value: T) => boolean;
  message: string,
}
