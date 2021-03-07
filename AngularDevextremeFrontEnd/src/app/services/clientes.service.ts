import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Clientes } from '../models/clientes';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {

  _URLClientes = environment.URLClientes;

  constructor(private http:HttpClient){    
  }
  getAll():Observable<Clientes[]>{
    return this.http.get<Clientes[]>(this._URLClientes);
  }
  getByFch(id:number,Edad:number,fchIni:Date,fchFnl:Date):Observable<Clientes[]>{  
    return this.http.get<Clientes[]>(`${this._URLClientes}/GetBy/${id}?Edad=${Edad}&fchIni=${fchIni.toISOString()}&fchFnl=${fchFnl.toISOString()}`);
  }
  getAllByFrecuencia():Observable<Clientes[]>{
    return this.http.get<Clientes[]>(`${this._URLClientes}/GetDateOfPurchase`);
  }
}
