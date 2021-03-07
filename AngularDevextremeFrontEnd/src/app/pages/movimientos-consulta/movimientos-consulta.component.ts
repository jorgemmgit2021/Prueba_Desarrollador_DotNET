import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DxFormModule } from 'devextreme-angular';
import { Observable } from 'rxjs';
import { MovimientosTotal } from 'src/app/models/-movimientos-total';
import { VentasService } from 'src/app/services/ventas.service';
import { environment } from 'src/environments/environment';
import ArrayStore from "devextreme/data/array_store";
import DataSource from "devextreme/data/data_source";
import { DxDataGridModule } from 'devextreme-angular/ui/data-grid';

@Component({
  selector: 'app-movimientos-consulta',
  templateUrl: './movimientos-consulta.component.html',
  styleUrls: ['./movimientos-consulta.component.scss']
})
export class MovimientosConsultaComponent implements OnInit {
  public _movimientos$:Observable<MovimientosTotal[]>;
  private _vService:VentasService;
  public _movimientos:MovimientosTotal[]=[];
  employees:MovimientosTotal[] = [];

  constructor(vService:VentasService){    
    this.employees = vService.getEmployees();
    this._vService = vService;
    this._movimientos$=vService.getAllMovimientos();
    this._movimientos$.subscribe(result=>{this._movimientos = result; })
    console.log('In');
  }

  ngOnInit(): void {    
  }
}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxDataGridModule
  ],
  declarations: [ MovimientosConsultaComponent ],
  exports: [ MovimientosConsultaComponent ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class MovimientosConsultaModule { }