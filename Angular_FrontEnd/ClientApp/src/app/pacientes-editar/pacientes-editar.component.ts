import { Component, Inject, OnInit } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { PacientesService } from "../servicios/pacientes.service";
import { DoctoresService } from '../servicios/doctores.service';
import { Pacientes } from "../models/Pacientes";
import { ActivatedRoute, Router } from "@angular/router";
import { ValueConverter } from '@angular/compiler/src/render3/view/template';
import { formatNumber } from '@angular/common';
import { ModalDialogComponent } from '../modal-dialog/modal-dialog.component';
import { MatDialog, MatDialogConfig, MatDialogRef, MatSnackBarConfig, MatSnackBarModule } from  '@angular/material';
import { Doctores } from '../models/doctores';
import { ControlIntegral } from '../models/control-integral';
import { Observable } from 'rxjs';
import { DialogInfo } from '../models/dialog-info';
import { config } from 'process';
import { MatSnackBar} from '@angular/material';
@Component({
  selector: 'app-Pacientes-editar',
  templateUrl: './Pacientes-editar.component.html',
  styleUrls: ['./Pacientes-editar.component.css']
})
export class PacientesEditarComponent implements OnInit {
  public c: Pacientes;
  public d$: Observable<Doctores[]>;
  public l$: Observable<Doctores[]>;
  public i:ControlIntegral;
  public notyfy:boolean;
  public toNotify:string;

  constructor(private service:PacientesService, private listService:DoctoresService, private route:ActivatedRoute, private router:Router,private  dialog: MatDialog, public snackBar:MatSnackBar){
    this.c = this.initType();
    this.i = this.initSub();
    this.notyfy = false;
    this.toNotify='';
  }
  initType(){
      this.c = { Id_Paciente:0,Nombre_Completo:'',Numero_Seguro_Social:'',Codigo_Postal:'',Telefono_Contacto:'', Control_Integral:{} as ControlIntegral[] };
      return this.c;
  }
  initSub(): ControlIntegral {
    this.i = { Id_Seguimiento:0, Id_Paciente:0, Id_Doctor:0,Fecha:'', Estado:false };
    return this.i;
  }
  start():void{
    this.i.Id_Paciente = this.c.Id_Paciente;
    this.dialog.open(ModalDialogComponent, {data:{ _i:this.i, _list: this.l$, _type:1 }});
    this.dialog.afterAllClosed.subscribe(x=>this.end(x));
  }
  end(x):void{
    let _cont:ControlIntegral[] =this.c.Control_Integral!=undefined?this.c.Control_Integral:[]; 
    let _x:ControlIntegral = { Id_Seguimiento: this.i.Id_Seguimiento, Id_Paciente:this.i.Id_Paciente, Id_Doctor:Number(this.i.Id_Doctor),Fecha:this.i.Fecha, Estado:this.i.Estado };
    if(this.c.Control_Integral.length==undefined)this.c.Control_Integral=[];
    let u = this.c.Control_Integral.find(function(indx){ return indx.Id_Doctor== _x.Id_Doctor && indx.Id_Paciente==_x.Id_Paciente; });
    if(u==undefined){
      if(_cont.unshift(_x)!=0)
      this.d$.subscribe((data)=>{data.push({ 
        Id_Doctor:_x.Id_Doctor, Nombre_Completo:"Adicionado",Especialidad:"",Numero_Credencial:0,Hospital_Adscrito:"",Control_Integral:[] 
      });console.log(data)});
      _x = null;
    }
    this.c.Control_Integral = _cont;
    _cont = null;
  }
  ngOnInit(){
    var _id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    if(_id!=0)this.service.Editar(_id).subscribe((data:Pacientes)=>{ this.c = data; console.log(this.c);});
    else this.c = this.initType();
      this.d$ = _id!=0? this.listService.getBy(_id):{} as Observable<Doctores[]>;
      this.l$ = this.listService.getAll();
  }
  public getPacientes(id){
    this.service.Editar(id).subscribe((data:Pacientes)=>{ if(this.c!=undefined) this.c = data; });
    console.log(this.c);
  }
  Guardar():void{
    this.toNotify = '';
    this.notyfy = null;
    let config = new MatSnackBarConfig();
    this.service.create(this.c).subscribe((data:Pacientes)=>{ this.c = data;
      this.toNotify= `Registro de Paciente completo`;
      this.snackBar.open(this.toNotify,'Cerrar', config);
    },error=>{
      // debugger
        this.toNotify= "Errores en los contenidos del formulario";//`Unable to save Pacientes: ${error.message}`;
        // config.panelClass=['background-red'];
        // config.announcementMessage="Announcement";
        this.snackBar.open(this.toNotify,'Cerrar', config);
      });
      this.notyfy=true;
      // this.router.navigate(['/pacientes']);
  }
}
