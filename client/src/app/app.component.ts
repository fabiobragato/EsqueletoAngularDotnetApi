import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ContaService } from './services/conta.service';
import { Usuario } from './models/usuario';
import { TranslateService } from '@ngx-translate/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = "Angular 10 CRUD Application";
  cols: any;

  constructor(
    private http: HttpClient,
    public contaService: ContaService,
    private translate: TranslateService,
    private config: PrimeNGConfig
  ) {
    this.translate.setDefaultLang('pt');
    this.translate.use('pt');
    this.translate.get('primeng').subscribe(res => this.config.setTranslation(res));
  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const usuarioString = localStorage.getItem('usuario');
    if (!usuarioString) return;
    const usuario: Usuario = JSON.parse(usuarioString);
    this.contaService.setCurrentUser(usuario);
  }

}
