import { AbstractControl, ValidatorFn } from '@angular/forms';

export function minLengthMasked(len: number): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const value = control.value ? control.value.replace(/\D/g, '') : '';
        return value.length < len ? { 'minLengthMasked': { value: control.value } } : null;
    };
}