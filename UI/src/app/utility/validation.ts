import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function MatchingPasswords(
  password1: string,
  password2: string
): ValidatorFn {
  return (formGroup: AbstractControl): ValidationErrors | null => {
    const control = formGroup.get(password1);
    const matchingControl = formGroup.get(password2);

    if (control?.value !== matchingControl?.value) {
      matchingControl?.setErrors({ mismatch: true });
      return { mismatch: true };
    } else {
      matchingControl?.setErrors(null);
      return null;
    }
  };
}

export function nonNegativeNumberValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    return value < 0 ? { negative: true } : null;
  };
}
