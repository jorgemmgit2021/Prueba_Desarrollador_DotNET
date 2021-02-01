import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import {Pacientes} from '../models/pacientes';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class PacientesService {  
  public baseURL = environment.pacientesURL;
  constructor(private httpClient: HttpClient, private router:Router){  }
  public getAll():Observable<Pacientes[]>{  
    return this.httpClient.get<Pacientes[]>(`${this.baseURL}`);
  }
  public getBy(id:number):Observable<Pacientes[]>{    
    return this.httpClient.get<Pacientes[]>(`${this.baseURL}/GetById/${id}`);
  }
  public Editar(id:number){
    return this.httpClient.get<Pacientes>(`${this.baseURL}/${id}`);
  }
  public create(data): Observable<any> {
    return this.httpClient.post(`${this.baseURL}`, data);
  }
  public async Delete(id:number){
    return await this.httpClient.delete<Pacientes>(`${this.baseURL}/${id}`);
  }
}