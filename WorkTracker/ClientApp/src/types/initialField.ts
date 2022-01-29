import { ValidationRule } from "./validationRule";

export interface InitialFormField<T> {
  name: string;
  required?: string;
  rules: ValidationRule<T>[];
}
