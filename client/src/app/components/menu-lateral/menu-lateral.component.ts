import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from 'src/app/models/usuario';
import { ContaService } from 'src/app/services/conta.service';

@Component({
  selector: 'app-menu-lateral',
  templateUrl: './menu-lateral.component.html',
  styleUrls: ['./menu-lateral.component.scss']
})
export class MenuLateralComponent {
  currentUser$: Observable<Usuario | null> = new Observable<Usuario | null>();
  constructor(public contaService: ContaService) { }

  visible = false;

  ngOnInit(): void {
    this.currentUser$ = new Observable<Usuario | null>();
  }

  loggout() {
    this.contaService.logout();
  }
}