import {Component, Inject, Injectable, Input} from  '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA, MatDialog} from  '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Doctores } from '../models/doctores';
import { ControlIntegral } from '../models/control-integral';
import { DialogInfo } from '../models/dialog-info';

@Component({
templateUrl:'modal-dialog.component.html'
})
export  class  ModalDialogComponent{
    @Input() id:string;
    private d$:Observable<Doctores[]>;
    private i:ControlIntegral;
    private l:Doctores[];
    private _type:number;
    constructor(private  dialogRef: MatDialogRef<ModalDialogComponent>, @Inject(MAT_DIALOG_DATA) public  data: DialogInfo){
      this.d$ = data._list
      this.l = {} as Doctores[];
      this.i=data._i;
      this.d$.subscribe((indata:Doctores[])=>{this.l = indata as Doctores[];console.log(this.l);});
    }
    ngOnInit(){
      this._type = this.data._type;
      this.dialogRef.updateSize("60%","60%px");            
    }
    public closeMe(){
        this.i.Fecha=new Date().toISOString();
        this.dialogRef.close(this.i);
    }
}