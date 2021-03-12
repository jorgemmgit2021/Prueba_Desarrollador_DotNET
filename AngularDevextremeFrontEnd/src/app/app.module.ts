import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { SideNavOuterToolbarModule, SideNavInnerToolbarModule, SingleCardModule } from './layouts';
import { FooterModule, ResetPasswordFormModule, CreateAccountFormModule, ChangePasswordFormModule, LoginFormModule } from './shared/components';
import { AuthService, ScreenService, AppInfoService } from './shared/services';
import { UnauthenticatedContentModule } from './unauthenticated-content';
import { AppRoutingModule } from './app-routing.module';
import { DxFormModule,    
        DxCheckBoxModule,
        DxSelectBoxModule,
        DxNumberBoxModule,
        DxDataGridModule,
        DxBulletModule,
        DxTemplateModule, 
        DxTextBoxModule} from 'devextreme-angular';
import { MovimientosModule } from './pages/movimientos/movimientos.component';
import { HttpClientModule } from '@angular/common/http';
import { MovimientosConsultaComponent, MovimientosConsultaModule } from './pages/movimientos-consulta/movimientos-consulta.component';
import DevExpress from 'devextreme';
import { InventariosConsultaModule } from './pages/inventario-consulta/inventario-consulta.component';
import { InventariosUnidadesMinModule } from './pages/inventarios-unidades-min/inventarios-unidades-min.component';
import { ClientesConsultaFchComponent, ClientesConsultaFchModule } from './pages/clientes-consulta-fch/clientes-consulta-fch.component';
import { ClientesConsultaFrecuenciaComponent } from './pages/clientes-consulta-frecuencia/clientes-consulta-frecuencia.component';

@NgModule({
  declarations: [
    AppComponent 
    ],
  imports: [    
    BrowserModule,
    SideNavOuterToolbarModule,
    SideNavInnerToolbarModule,
    SingleCardModule,
    FooterModule,
    ResetPasswordFormModule,
    CreateAccountFormModule,
    ChangePasswordFormModule,
    LoginFormModule,
    UnauthenticatedContentModule,
    AppRoutingModule,
    BrowserModule,
    DxCheckBoxModule,
    DxSelectBoxModule,
    DxNumberBoxModule,
    DxFormModule,
    DxDataGridModule,
    DxBulletModule,
    DxTemplateModule,
    DxTextBoxModule,
    DxNumberBoxModule,
    MovimientosModule,
    MovimientosConsultaModule,
    HttpClientModule,
    InventariosConsultaModule,
    InventariosUnidadesMinModule,
    ClientesConsultaFchModule
  ],
  providers: [AuthService, ScreenService, AppInfoService],
  bootstrap: [AppComponent],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
