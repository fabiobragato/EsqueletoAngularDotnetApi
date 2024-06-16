import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PaginaInicialComponent } from './components/pagina-inicial/pagina-inicial.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: 'home', component: PaginaInicialComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', component: PaginaInicialComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
