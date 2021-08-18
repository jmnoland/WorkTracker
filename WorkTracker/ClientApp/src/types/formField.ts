import { ValidationRule } from "./validationRule";
import { Error } from "./error";

export interface FormField<T> {
  name: string;
  value: T;
  errors: Error[];
  required?: string;
  validation: { rules: ValidationRule<T>[] };
  onChange: (val: T) => void;
}
