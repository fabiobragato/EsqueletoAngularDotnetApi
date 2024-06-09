import { Component, OnInit } from '@angular/core';
import { ContaService } from '../../_services/conta.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { Usuario } from 'src/app/_models/usuario';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  model: any = {};
  isActive = false;

  constructor(public contaService: ContaService) { }

  ngOnInit(): void {
  }

  login() {
    this.contaService.login(this.model).subscribe({
      next: response => {
        console.log("Logado com sucesso!");
      },
      error: error => console.log(error)
    });
  }

  loggout() {
    this.contaService.logout();
  }

  // Lógica para mostrar/esconder o formulário de login/registrar
  toggleActive(active: boolean) {
    this.isActive = active;
  }

  // Lógica para criar formulário de registro
  formGroupRegistro = new FormGroup({
    nome: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    senha: new FormControl('', [Validators.required, Validators.minLength(6)])
  });

  // Método para verificar se a senha contém uma letra minúscula
  senhaContemMinuscula() {
    const controleSenha = this.formGroupRegistro.get('senha');
    if (controleSenha) {
      const senha = controleSenha.value;
      if (senha !== null) {
        return /[a-z]/.test(senha);
      }
    }
    return false;
  }

  // Método para verificar se a senha contém uma letra maíscula
  senhaContemMaiscula() {
    const controleSenha = this.formGroupRegistro.get('senha');
    if (controleSenha) {
      const senha = controleSenha.value;
      if (senha !== null) {
        return /[A-Z]/.test(senha);
      }
    }
    return false;
  }

  // Método para verificar se a senha contém número	
  senhaContemNumero() {
    const controleSenha = this.formGroupRegistro.get('senha');
    if (controleSenha) {
      const senha = controleSenha.value;
      if (senha !== null) {
        return /\d/.test(senha);
      }
    }
    return false;
  }

  // Método para verificar se a senha contém 6 caracteres
  senhaTemMinimoSeisCaracteres() {
    const controleSenha = this.formGroupRegistro.get('senha');
    if (controleSenha) {
      const senha = controleSenha.value;
      if (senha !== null) {
        return senha.length >= 6;
      }
    }
    return false;
  }

}
