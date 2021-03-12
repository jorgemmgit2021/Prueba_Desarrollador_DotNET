import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DxButtonModule, DxFormModule, DxSelectBoxModule, DxDataGridModule, DxCalendarModule } from 'devextreme-angular';
import { Observable } from 'rxjs';
import { MovimientosTotal } from 'src/app/models/-movimientos-total';
import { VentasService } from 'src/app/services/ventas.service';
import { environment } from 'src/environments/environment';
import ArrayStore from "devextreme/data/array_store";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: 'app-movimientos-consulta',
  templateUrl: './movimientos-consulta.component.html',
  styleUrls: ['./movimientos-consulta.component.scss']
})
export class MovimientosConsultaComponent implements OnInit {
  public _movimientos$:Observable<MovimientosTotal[]>;
  private _vService:VentasService;
  public _movimientos:MovimientosTotal[]=[];
  public n:number[] = [];
  i=0;
  currentValue:Date=new Date();
  constructor(vService:VentasService){    
    this._vService = vService;
    for(this.i=1995;this.i<2051;this.i++){this.n=[...this.n,this.i]}
    this.i=null;
  }
  query=()=>{
    this._movimientos$=this._vService.getAllMovimientos(this.i||this.currentValue.getFullYear());
    this._movimientos$.subscribe(result=>{this._movimientos = result; });
    console.log('In');
    this.i = null;
  }
  change=(e)=>{
    this.i = e.value;
  }
  ngOnInit(): void {
  }
}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxDataGridModule,
    DxButtonModule,
    DxSelectBoxModule,
    DxCalendarModule
  ],
  declarations: [ MovimientosConsultaComponent ],
  exports: [ MovimientosConsultaComponent ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class MovimientosConsultaModule { }