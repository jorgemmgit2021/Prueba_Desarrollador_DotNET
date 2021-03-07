import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Inventarios } from '../models/inventarios';

@Injectable({
  providedIn: 'root'
})
export class InventariosService {
 URLInventarios = environment.URLInventarios;

  constructor(private http:HttpClient){    
  }
  getAll():Observable<Inventarios[]>{
    return this.http.get<Inventarios[]>(this.URLInventarios);
  }
  getItem(id:number):Observable<Inventarios[]>{
    return this.http.get<Inventarios[]>(`${this.URLInventarios}/${id}`);
  }
  getAllUnidadesMin():Observable<Inventarios[]>{
    return this.http.get<Inventarios[]>(`${this.URLInventarios}/GetAllStockMin`);
  }
  getVentasXItem():Observable<Inventarios[]>{
    return this.http.get<Inventarios[]>(`${this.URLInventarios}/GetAllVentasXItem`);
  }
}
