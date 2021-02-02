import {Component, Inject, Injectable, Input} from  '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA, MatDialog} from  '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Doctores } from '../models/doctores';
import { ControlIntegral } from '../models/control-integral';
import { DialogInfo } from '../models/dialog-info';
import { Pacientes } from '../models/pacientes';

@Component({
templateUrl:'modal-dialog.component.html'
})
export  class  ModalDialogComponent{
    @Input() id:string;
    private d$:Observable<Doctores[]>;
    private p$:Observable<Pacientes[]>;
    private i:ControlIntegral;
    private l:Doctores[];
    private t:Pacientes[];
    private _type:number;
    constructor(private  dialogRef: MatDialogRef<ModalDialogComponent>, @Inject(MAT_DIALOG_DATA) public  data: DialogInfo){
      this.l = [];
      this.t = [];
      this.i=data._i;
      this._type = data._type;
      if(this._type==1){
        this.l=this.data._listD;
      }
      if(this._type==0){
        this.t = this.data._listP;        
      }
    }
    ngOnInit(){
      this._type = this.data._type;
      this.dialogRef.updateSize("60%","90%");            
    }
    public closeMe(){
      debugger
        this.i.Fecha=new Date().toISOString();
        this.dialogRef.close(this.i);
    }
}