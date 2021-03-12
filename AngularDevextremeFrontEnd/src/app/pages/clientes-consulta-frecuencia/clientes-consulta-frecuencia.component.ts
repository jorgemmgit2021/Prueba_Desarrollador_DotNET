import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { Component, NgModule, OnInit } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DxDataGridModule } from 'devextreme-angular';
import { ClientesService } from 'src/app/services/clientes.service';

@Component({
  selector: 'app-clientes-consulta-frecuencia',
  templateUrl: './clientes-consulta-frecuencia.component.html',
  styleUrls: ['./clientes-consulta-frecuencia.component.scss']
})
export class ClientesConsultaFrecuenciaComponent implements OnInit {
  public InfoCliente:any[]=[];
  constructor(private cService:ClientesService){ 
    cService.getAllByFrecuencia().subscribe(resp=>{ this.InfoCliente=resp; });
  }

  ngOnInit(): void {
  }

}
@NgModule({
  imports: [
    BrowserModule,
    DxDataGridModule
],
  declarations:[ClientesConsultaFrecuenciaComponent],
  exports:[ClientesConsultaFrecuenciaComponent],
  schemas:[CUSTOM_ELEMENTS_SCHEMA,NO_ERRORS_SCHEMA]
}) 
export class ClientesConsultaFrecuenciaModule{
}
