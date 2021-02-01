import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PacientesComponent } from './pacientes/pacientes.component';
import { DoctoresComponent } from './doctores/doctores.component';
import { PacientesService } from './servicios/pacientes.service';
import { DoctoresService } from './servicios/doctores.service';
import { PacientesEditarComponent } from './pacientes-editar/pacientes-editar.component';
import { DoctoresEditarComponent } from './doctores-editar/doctores-editar.component';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBar, MatSnackBarModule, SimpleSnackBar } from '@angular/material/snack-bar';
import { ModalDialogComponent } from './modal-dialog/modal-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PacientesComponent,
    DoctoresComponent,
    PacientesEditarComponent,
    DoctoresEditarComponent,
    ModalDialogComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    MatDialogModule, 
    MatInputModule, 
    MatButtonModule, 
    MatCardModule, 
    MatFormFieldModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'pacientes', component: PacientesComponent },
      { path: 'doctores', component: DoctoresComponent },
      { path: 'editar-paciente/:id', component: PacientesEditarComponent, data: { animation: { value: 'paciente' } } },
      { path: 'editar-doctor/:id', component: DoctoresEditarComponent, data: { animation: { value: 'doctor' } } }
    ]),
    MatSnackBarModule
  ],
  providers: [PacientesService,DoctoresService],
  bootstrap: [AppComponent],
  schemas:[CUSTOM_ELEMENTS_SCHEMA],
  entryComponents:[ModalDialogComponent]
})
export class AppModule { }
