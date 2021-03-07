import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DxDataGridModule, DxFormModule } from 'devextreme-angular';
import { Inventarios } from 'src/app/models/inventarios';
import { InventariosService } from 'src/app/services/inventarios.service';

@Component({
  selector: 'app-inventario-consulta',
  templateUrl: './inventario-consulta.component.html',
  styleUrls: ['./inventario-consulta.component.scss']
})
export class InventariosConsultaComponent implements OnInit {
  _Inventarios:Inventarios[]=[];
  _iService:InventariosService;
  constructor(iService:InventariosService){ 
    this._iService = iService;
  }

  ngOnInit(): void {
    this._iService.getAll().toPromise().then(data=>{ this._Inventarios = data; });
  }

}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxDataGridModule
  ],
  declarations: [ InventariosConsultaComponent ],
  exports: [ InventariosConsultaComponent ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class InventariosConsultaModule { }