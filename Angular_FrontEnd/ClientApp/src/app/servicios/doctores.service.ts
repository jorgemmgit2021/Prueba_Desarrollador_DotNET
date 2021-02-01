import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Doctores } from '../models/doctores';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DoctoresService {
  public baseURL = environment.doctoresURL;
  public d:Doctores[];
  constructor(private httpClient:HttpClient, private route:ActivatedRoute, private router:Router){}
  public getAll():Observable<Doctores[]>{    
    return this.httpClient.get<Doctores[]>(`${this.baseURL}`);
  }
  public getBy(id:number):Observable<Doctores[]>{    
    return this.httpClient.get<Doctores[]>(`${this.baseURL}/GetById/${id}`);
  }
  public Editar(id:number){
      return this.httpClient.get<Doctores>(`${this.baseURL}/${id}`);
  }
  public Create(data):Observable<Doctores>{    
    return this.httpClient.post<Doctores>(`${this.baseURL}`,data);    
  }
  public async Delete(id:number){
    return await this.httpClient.get(`${this.baseURL}/Delete/${id}`);
  }
}