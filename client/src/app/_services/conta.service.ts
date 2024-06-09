import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Usuario } from '../_models/usuario';

@Injectable({
  providedIn: 'root'
})
export class ContaService {

  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new BehaviorSubject<Usuario | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<Usuario>(this.baseUrl + 'conta/login', model).pipe(
      map((response: Usuario) => {
        const user = response;
        if (user) {
          localStorage.setItem('usuario', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
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
