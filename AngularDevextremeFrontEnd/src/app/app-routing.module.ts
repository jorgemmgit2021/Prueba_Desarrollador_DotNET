import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent, ResetPasswordFormComponent, CreateAccountFormComponent, ChangePasswordFormComponent } from './shared/components';
import { AuthGuardService } from './shared/services';
import { HomeComponent } from './pages/home/home.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { DxDataGridModule, DxFormModule } from 'devextreme-angular';
import { MovimientosComponent } from './pages/movimientos/movimientos.component';
import { MovimientosConsultaComponent } from './pages/movimientos-consulta/movimientos-consulta.component';
import { InventariosConsultaComponent } from './pages/inventario-consulta/inventario-consulta.component';
import { InventariosUnidadesMinComponent } from './pages/inventarios-unidades-min/inventarios-unidades-min.component';
import { ClientesConsultaFchComponent } from './pages/clientes-consulta-fch/clientes-consulta-fch.component';
import { ClientesConsultaFrecuenciaComponent } from './pages/clientes-consulta-frecuencia/clientes-consulta-frecuencia.component';

const routes: Routes = [
  {
    path: 'tasks',
    component: TasksComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path:'ventas',
    component: MovimientosComponent,
    canActivate:[AuthGuardService]   
  },
  {
    path:'movimientos',
    component:MovimientosConsultaComponent,
    canActivate:[AuthGuardService]    
  },
  {
    path:'listado',
    component:InventariosConsultaComponent
  },
  {
    path:'items',
    component:InventariosUnidadesMinComponent
  },
  {
    path:'historial',
    component:ClientesConsultaFchComponent
  },
  {
    path:'registro',
    component:ClientesConsultaFrecuenciaComponent
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'login-form',
    component: LoginFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'reset-password',
    component: ResetPasswordFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'create-account',
    component: CreateAccountFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: 'change-password/:recoveryCode',
    component: ChangePasswordFormComponent,
    canActivate: [ AuthGuardService ]
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), DxDataGridModule, DxFormModule],
  providers: [AuthGuardService],
  exports: [RouterModule],
  declarations: [HomeComponent, ProfileComponent, TasksComponent]
})
export class AppRoutingModule { }
