import { FormField } from './formField';

export interface Form {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  form: Record<string, FormField<any>>;
  modified: string[];
  validate: () => boolean;
  reset: () => void;
}
