import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ContaService } from '../../services/conta.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { matchValues } from 'src/app/validators/match-values.validator';
import { minLengthMasked } from 'src/app/validators/min-length.validator';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  registroForm: FormGroup = new FormGroup({});
  loginForm: FormGroup = new FormGroup({});
  isActive = false;

  constructor(public contaService: ContaService, private messageService: MessageService) { }

  ngOnInit(): void {
    this.iniciandoRegistroForm();
    this.iniciandoLoginForm();
  }

  // Login
  iniciandoLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      senha: new FormControl('', Validators.required)
    });
  }

  login() {
    this.contaService.login(this.loginForm.value).subscribe({
      next: response => {
        this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Logado com sucesso' });
      },
      error: error => {
        if (typeof error === 'object') {
          this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Falha no login. Tente novamente!' });
        } else {
          const errorMessage = error.message.replace('Error: ', '');
          this.messageService.add({ severity: 'error', summary: 'Erro', detail: errorMessage });
        }
      }
    });
  }

  // Logout
  loggout() {
    this.contaService.logout();
  }

  // Registro
  iniciandoRegistroForm() {
    this.registroForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      senha: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmarSenha: new FormControl('', [Validators.required, matchValues('senha')]),
      celular: new FormControl('', [Validators.required, minLengthMasked(11)])
    });

    this.registroForm.controls['senha'].valueChanges.subscribe(() => {
      next: () => this.registroForm.controls['confirmarSenha'].updateValueAndValidity();
    });
  }

  registro() {
    this.contaService.registro(this.registroForm.value).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Usuário cadastrado com sucesso' });
        this.cancel();
      },
      error: error => {
        const errorMessage = error.message.replace('Error: ', '');
        this.messageService.add({ severity: 'error', summary: 'Erro', detail: errorMessage });
      }
    });
  }

  // Cancelar
  cancel() {
    this.cancelRegister.emit(false);
  }

  // Lógica para mostrar/esconder o formulário de login/registrar
  toggleActive(active: boolean) {
    this.isActive = active;
  }

  // Método para verificar se a senha contém 6 caracteres
  senhaTemMinimoSeisCaracteres() {
    return this.registroForm.get('senha')?.valid || false;
  }

  // Método para verificar se o e-mail é válido
  emailValido() {
    return this.registroForm.get('email')?.valid || false;
  }

  // Método para verificar se o celular é válido
  celularValido() {
    return this.registroForm.get('celular')?.valid || false;
  }

  // Método para verificação de senha igual a confirmação de senha
  senhasIguais(): boolean {
    return this.registroForm.get('confirmarSenha')?.valid || false;
  }

}
