import { Component, OnInit } from '@angular/core';
import { PacientesService } from '../servicios/pacientes.service';
import { Pacientes } from '../models/Pacientes';
import { Router } from '@angular/router';
@Component({
  selector: 'app-Pacientess',
  templateUrl:'./Pacientes.component.html',
  styleUrls: ['./Pacientes.component.css']
})
export class PacientesComponent implements OnInit {
  public c: Pacientes;
  public f: Pacientes[];
  constructor(public service:PacientesService, private router:Router) { }
  ngOnInit(){
    this.service.getAll().subscribe((data:Pacientes[])=>{ this.f = data; console.log(this.f); });
  }
  public Editar(id){
    this.service.Editar(id).subscribe((data:Pacientes)=>{ this.c = data;  console.log(this.f);});
  }
  public async Eliminar(id){    
    (await this.service.Delete(id)).subscribe((data: Pacientes) => { this.c = data; console.log(data); });
    this.router.navigate(['/Pacientes']);
  }
}
