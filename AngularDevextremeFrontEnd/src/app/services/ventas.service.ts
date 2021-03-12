import { HttpClient, HttpHandler, HttpXhrBackend } from '@angular/common/http';
import { Injector } from '@angular/core';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MovimientosTotal } from '../models/-movimientos-total';
import { Clientes } from '../models/clientes';
import { Inventarios } from '../models/inventarios';
import { Movimientos } from '../models/movimientos';
import { Venta } from '../models/venta';

@Injectable({
  providedIn: 'root'
})
export class VentasService {
  public _movimientos:Movimientos;
  private baseURL=environment.URLMovimientos;
  private totalesURL=environment.URLMovimientosTotales;
  private _URLInventarios=environment.URLInventarios;
  _URLClientes=environment.URLClientes;
  public _Inventarios:Inventarios[]=[];
  constructor(private httpClient:HttpClient, private route:ActivatedRoute, private router:Router){}
  getFromAsync(_number:string){ 
  //   var injector = Injector.create({
  //     providers: [
  //         { provide: HttpClient, deps: [HttpHandler] },
  //         { provide: HttpHandler, useValue: new HttpXhrBackend({ build: () => new XMLHttpRequest }) },
  //     ],
  // });
  // var _c:Clientes;
  // var httpClient: HttpClient = injector.get(HttpClient);
  // httpClient.get<Clientes>(`${this._URLClientes}/Get/${_number}`).toPromise().then(f=>{ _c=f;console.log(f); console.warn('#$%&/'); });
  // console.log('&/()=');
  // return _c;
    return this.httpClient.get<Clientes[]>(this._URLClientes);
  }
  public getAll():Observable<Movimientos>{    
    return this.httpClient.get<Movimientos>(`${this.baseURL}`);
  }
  public getAllMovimientos(per:number):Observable<MovimientosTotal[]>{
    return this.httpClient.get<MovimientosTotal[]>(`${this.totalesURL}?periodo=${per}`);
  }
  getAllInventarios():Observable<Inventarios[]>{
    return this.httpClient.get<Inventarios[]>(`${this._URLInventarios}`);
  }
  getCliente(identificacion:string){
    let cliente:Clientes=null;
    this.httpClient.get<Clientes>(`${this._URLClientes}/Get/${identificacion}`).toPromise().then(data=>{cliente=data});
    return cliente;
  }
  getEmployees(): MovimientosTotal[]{
    let _emp:MovimientosTotal[]=[];
    this.httpClient.get<MovimientosTotal[]>(`${this.totalesURL}?periodo=2020`).toPromise().then(data=>{_emp=data;});
    return _emp;
  }
  public async Guardarventa(_venta:Venta):Promise<Venta>{
    var _result:boolean = false;
    return this.httpClient.post<Venta>(`${this.baseURL}`, _venta).toPromise();
  }

}
