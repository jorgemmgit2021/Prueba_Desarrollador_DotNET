import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DxFormModule, DxButtonModule, DxCalendarModule, DxDataGridModule, DxDateBoxModule, DxNumberBoxModule } from 'devextreme-angular';
import { Clientes } from 'src/app/models/clientes';
import { ClientesBusqueda } from 'src/app/models/clientes-busqueda';
import { ClientesService } from 'src/app/services/clientes.service';

@Component({
  selector: 'app-clientes-consulta-fch',
  templateUrl: './clientes-consulta-fch.component.html',
  styleUrls: ['./clientes-consulta-fch.component.scss']
})
export class ClientesConsultaFchComponent implements OnInit {
  _Clientes:Clientes[]=[];
  _ClientesBusqueda:ClientesBusqueda;
  _cService:ClientesService;
  constructor(cService:ClientesService){
    this._cService = cService;
  }

  ngOnInit(): void{
    this._ClientesBusqueda = { Id:0, Edad:0, FchIni:new Date(),FchFnl:new Date() };
  }
  getByParams=(e)=>{
    let id=this._ClientesBusqueda.Id;
    let Edad=this._ClientesBusqueda.Edad;
    let fchIni:Date=this._ClientesBusqueda.FchIni;
    let fchFnl:Date=this._ClientesBusqueda.FchFnl;
    if(Edad==0||fchIni==new Date())return;
    this._cService.getByFch(id,Edad,fchIni,fchFnl).toPromise().then(data=>{ this._Clientes = data; });
  }
}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxDataGridModule,
    DxButtonModule,
    DxNumberBoxModule,
    DxCalendarModule,
    DxDateBoxModule
  ],
  declarations: [ ClientesConsultaFchComponent ],
  exports: [ ClientesConsultaFchComponent ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class ClientesConsultaFchModule { }
