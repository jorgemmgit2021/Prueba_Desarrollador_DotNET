import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DxDataGridModule, DxFormModule } from 'devextreme-angular';
import { Inventarios } from 'src/app/models/inventarios';
import { InventariosService } from 'src/app/services/inventarios.service';

@Component({
  selector: 'app-inventarios-unidades-min',
  templateUrl: './inventarios-unidades-min.component.html',
  styleUrls: ['./inventarios-unidades-min.component.scss']
})
export class InventariosUnidadesMinComponent implements OnInit {

  _Inventarios:Inventarios[]=[];
  _iService:InventariosService;
  constructor(iService:InventariosService){ 
    this._iService = iService;
  }

  ngOnInit(): void {
    this._iService.getAllUnidadesMin().toPromise().then(data=>{ this._Inventarios = data; });
  }

}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxDataGridModule
  ],
  declarations: [ InventariosUnidadesMinComponent ],
  exports: [ InventariosUnidadesMinComponent ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class InventariosUnidadesMinModule { }