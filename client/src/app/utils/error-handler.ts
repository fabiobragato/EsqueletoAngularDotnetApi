import { HttpErrorResponse } from '@angular/common/http';

export function handleError(error: HttpErrorResponse) {
    let errorMessage = 'Erro desconhecido!';
    if (error.error instanceof ErrorEvent) {
        // Erro no lado do cliente
        errorMessage = `Erro: ${error.error.message}`;
    } else {
        // Erro no lado do servidor
        errorMessage = `Código do erro: ${error.status}\nMensagem: ${error.message}`;
    }
    console.error(errorMessage);
    return new Error(errorMessage);
}