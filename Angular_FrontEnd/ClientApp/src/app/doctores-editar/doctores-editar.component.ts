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
  selector: 'app-doctores-editar',
  templateUrl: './doctores-editar.component.html',
  styleUrls: ['./doctores-editar.component.css']
})
export class DoctoresEditarComponent implements OnInit {
  public d: Doctores;
  public p$: Observable<Pacientes[]>;
  public l$: Observable<Pacientes[]>;
  public i:ControlIntegral;
  public notyfy:boolean;
  public toNotify:string;

  constructor(private service:DoctoresService, private listService:PacientesService, private route:ActivatedRoute, private router:Router,private  dialog: MatDialog, public snackBar:MatSnackBar){
    this.d = this.initType();
    this.i = this.initSub();
    this.notyfy = false;
    this.toNotify='';
  }
  initType(){
      this.d = { Id_Doctor:0,Nombre_Completo:'',Especialidad:'',Numero_Credencial:0,Hospital_Adscrito:'', Control_Integral:[] };
      return this.d;
  }
  initSub(): ControlIntegral {
    this.i = { Id_Seguimiento:0, Id_Paciente:0, Id_Doctor:0,Fecha:'', Estado:false };
    return this.i;
  }
  start():void{
    this.i.Id_Doctor = this.d.Id_Doctor;
    this.dialog.open(ModalDialogComponent, {data:{ _i:this.i, _list: this.l$, _type:1 }});
    this.dialog.afterAllClosed.subscribe(x=>this.end(x));
  }
  end(x):void{
    this.d.Control_Integral==this.d.Control_Integral||[];
    let _cont:ControlIntegral[] = this.d.Control_Integral;
    let _x:ControlIntegral = { Id_Seguimiento: this.i.Id_Seguimiento,Id_Paciente :Number(this.i.Id_Paciente), Id_Doctor:this.i.Id_Doctor,Fecha:this.i.Fecha, Estado:this.i.Estado };
    let u = this.d.Control_Integral.find(function(indx){ return indx.Id_Paciente== _x.Id_Paciente && indx.Id_Doctor==_x.Id_Doctor; });
    if(u==undefined&&_cont!=undefined){
      if(_cont.unshift(_x)!=0)
      this.p$.subscribe((data)=>{data.push({ 
        Id_Paciente:Number(_x.Id_Paciente),Nombre_Completo:"Adicionado", Numero_Seguro_Social:"",Codigo_Postal:"",Telefono_Contacto:"",Control_Integral:[]
      });console.log(data)});
      _x = null;
    }
    this.d.Control_Integral = _cont;
    _cont = null;
  }
  ngOnInit(){
    var _id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    if(_id!=0)this.service.Editar(_id).subscribe((data:Doctores)=>{ this.d = data; console.log(this.d);});
    else {this.d = this.initType();}
      this.p$ = _id!=0? this.listService.getBy(_id):{} as Observable<Pacientes[]>;
      this.l$ = this.listService.getAll();
      console.log(this.p$);
      console.log(this.l$);
  }
  public getDoctores(id){
    this.service.Editar(id).subscribe((data:Doctores)=>{ if(this.d!=undefined) this.d = data; });
    console.log(this.d);
  }
  Guardar():void{
    this.toNotify = '';
    this.notyfy = null;
    let config = new MatSnackBarConfig();
    this.service.Create(this.d).subscribe((data:Doctores)=>{ this.d = data;
      this.toNotify= `Registro de Doctor completo`;
      this.snackBar.open(this.toNotify,'Cerrar', config);
    },error=>{
        this.toNotify= "Errores en los contenidos del formulario";//`Unable to save Doctores: ${error.message}`;
        this.snackBar.open(this.toNotify,'Cerrar', config);
      });
      this.notyfy=true;
  }
}
