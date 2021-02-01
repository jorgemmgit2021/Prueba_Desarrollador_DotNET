import { Component, OnInit } from '@angular/core';
import { DoctoresService } from '../servicios/doctores.service';
import { Doctores } from '../models/doctores';
import { Router } from '@angular/router';
@Component({
  selector: 'app-doctores',
  templateUrl:'./doctores.component.html',
  styleUrls: ['./doctores.component.css']
})
export class DoctoresComponent implements OnInit {
  public c: Doctores;
  public f: Doctores[];
  constructor(public service:DoctoresService, private router:Router) { }
  ngOnInit(){
    this.service.getAll().subscribe((data:Doctores[])=>{ this.f = data; console.log(this.f); });
  }
  public Editar(id){
    this.service.Editar(id).subscribe((data:Doctores)=>{ this.c = data;  console.log(this.f);});
  }
  public async Eliminar(id){    
    (await this.service.Delete(id)).subscribe((data: Doctores) => { this.c = data; console.log(data); });
    this.router.navigate(['/Doctores']);
  }
}
