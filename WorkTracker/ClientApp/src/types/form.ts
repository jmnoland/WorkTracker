import { Dictionary } from './dictionary';
import { FormField } from './formField';

export interface Form {
  form: Dictionary<FormField<any>>;
  modified: string[];
  validate: () => boolean;
  reset: () => void;
}
