import { AbstractControl, ValidatorFn } from '@angular/forms';

export function matchValues(matchTo: string): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    return !!control.parent &&
      !!control.parent.value &&
      control.value === control.parent.get(matchTo)?.value
      ? null
      : { isMatching: true };
};
}
