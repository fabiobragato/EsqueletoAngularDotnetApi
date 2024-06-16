import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, tap, ObservableInput } from 'rxjs';
import { Usuario } from '../models/usuario';
import { Login } from '../models/login';
import { environment } from '../enviroments/enviroment';
import { Registro } from '../models/registro';

@Injectable({
  providedIn: 'root'
})
export class ContaService {

  baseUrl = environment.baseUrl;
  api = environment.apiContas;
  private currentUserSource = new BehaviorSubject<Usuario | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: Login): Observable<void> {
    return this.http.post<Usuario>(`${this.baseUrl}${this.api}logins`, model).pipe(
      tap((response: Usuario) => {
        if (response) {
          localStorage.setItem('usuario', JSON.stringify(response));
          this.currentUserSource.next(response);
        }
      }),
      catchError((error: HttpErrorResponse): ObservableInput<any> => {
        throw new Error(error.error);
      })
    );
  }

  registro(model: Registro) {
    return this.http.post<Usuario>(`${this.baseUrl}${this.api}registros`, model).pipe(
      map((user: Usuario) => {
        if (user) {
          localStorage.setItem('usuario', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      }),
      catchError((error: HttpErrorResponse): ObservableInput<any> => {
        throw new Error(error.error);
      })
    );
  }

  setCurrentUser(user: Usuario) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('usuario');
    this.currentUserSource.next(null);
  }

}
