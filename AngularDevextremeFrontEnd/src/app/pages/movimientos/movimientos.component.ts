import { NgModule, Component, Pipe, PipeTransform, enableProdMode, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { DxDataGridModule,
         DxPopupModule, 
         DxTemplateModule,
         DxFormModule, 
         DxLoadIndicatorModule,
         DxButtonModule,
         DxTextBoxModule } from 'devextreme-angular';
import notify from 'devextreme/ui/notify';
import { VentasService } from '../../services/ventas.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { Venta } from '../../models/venta';
import { Inventarios } from 'src/app/models/inventarios';
import { DetalleMovimientos } from 'src/app/models/detalle-movimientos';
import { Clientes } from 'src/app/models/clientes';
import { Observable } from 'rxjs';
import { Movimientos } from 'src/app/models/movimientos';

@Component({
  selector: 'app-movimientos',
  templateUrl: './movimientos.component.html',
  styleUrls: ['./movimientos.component.scss']
})
export class MovimientosComponent {
  public datasource:DetalleMovimientos[];
  public _datasource:any[];
  formData: any = {};
  public venta:Venta;
  public cliente:Clientes;
  public _clientes:Clientes[];  
  public _vService:VentasService;
  public popupVisible=false;
  public _validaItem;
  _inventario: Inventarios[];
  public tipo:any;
  constructor(private vService:VentasService, private router: Router){
    this._vService = vService;
    this.venta = {
      cliente:{ Id_Cliente:0, Numero_identificacion:0, Tipo_Identificacion:0, Nombre_completo:'', Fecha_nacimiento:'' }, 
      inventario:{} as Inventarios[], 
      movimiento:{ Id_Movimiento:0, Id_Cliente:0, Fecha:'', Total:'', Estado:true, Tipo_movimiento:1, Detalle_Movimiento:{} as DetalleMovimientos[] }
    };
    vService.getAllInventarios().subscribe(result=>{this._inventario=result; });
    this.venta.inventario = this._inventario;
    this.datasource=[];
    this.cliente= this.venta.cliente;
    vService.getFromAsync('').subscribe(result=>{this._clientes=result; });
    this.tipo=[{Id:1,Descripcion:'Cédula de ciudadanía'},{Id:2,Descripcion:'Otros'}];
    this.calculateCellValue = this.calculateCellValue.bind(this);
    this._datasource = [];
  }

  onSaving(e: any){
    const change = e.changes[0];
    if(change && change.type=="insert"){
        e.cancel = true;
        var _itm = change.data.Id_Item;
        var _ctd = change.data.Cantidad;
        var _ttl = this.calculateCellValue(change.data);
        var tr = eval(`this._datasource.push({Id:0,Id_Movimiento:0,Id_Detalle:0,Id_Item:${_itm},Cantidad:${_ctd},Total:${_ttl}})`);
    }
}
  calculateCellValue(data){
    if(data.Id_Item!=undefined && data.Id_Item!=0){
      var _item = this._inventario.filter(a=>a.Id_Item==data.Id_Item)[0];
      var _cantidad = new Number(_item.Cantidad_stock).valueOf();
      if(data.Cantidad<=_cantidad){
        var _precio = new Number(_item.Precio).valueOf();
        var a = Math.abs(data.Cantidad) * _precio;
        this._validaItem = 'ok';
        return a.toString();
      }
      else{        
        notify(`El producto ${_item.Descripcion} tiene ${_cantidad} existencias disponibles`, 'error', 600);
        this._validaItem = 'error';
        return '';
      }
    }
    return '';
  }

  getDisplayExpr(item){
    if(!item) {
        return "";
    }
    return `Producto: ${item.Descripcion} Precio:${item.Precio} Stock:${item.Cantidad_stock}`;
}

  showInfo(cliente){
    debugger
    this.popupVisible = true;
    this.vService.getFromAsync(cliente.Numero_identificacion).toPromise().then(resp=>{this._clientes = resp;});
  }

  selectionChanged(data: any){
    this.cliente = data.value;
  }
  selectionChangedHandler(e) {
    this.cliente = e.selectedRowsData[0];
}
  collapsed=false;
    async onSubmit(e){
      e.preventDefault()
      var _Total = 0;
      this._datasource.forEach(h=>{
        this.datasource.push({Id_Detalle:0,Id_Movimiento:0,Id_Item:h.Id_Item, Cantidad:h.Cantidad,Estado:true});
        _Total+=h.Total;
      });
      this.venta.cliente = this.cliente;
      this.venta.movimiento = {Id_Movimiento:0, Id_Cliente:this.cliente.Id_Cliente, 
      Fecha : new Date(Date.now().valueOf()).toISOString() , Total:_Total.toString(),
      Estado:true, Tipo_movimiento:1, Detalle_Movimiento : this.datasource };
      await this.vService.Guardarventa(this.venta).then(()=>{notify('Registro completo');}, onRejected=>{notify('Error durante el registro');});
    }
  }
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule,
    DxButtonModule,
    DxTextBoxModule,
    DxDataGridModule,
    DxPopupModule, 
    DxTemplateModule
  ],
  declarations: [ MovimientosComponent ],
  exports: [ MovimientosComponent ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class MovimientosModule { }
