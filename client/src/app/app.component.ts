import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = "Angular 10 CRUD Application";
  usuarios: any;
  cols: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<any>('https://localhost:5001/api/Usuarios').subscribe({
      next: response => {
        this.usuarios = response;
        this.cols = [
          { field: 'id', header: 'ID' },
          { field: 'nome', header: 'Nome' },
          { field: 'email', header: 'E-mail' }
        ];
      },
      error: error => console.error(error),
      complete: () => console.log('Request completed')
    });
  }

}
