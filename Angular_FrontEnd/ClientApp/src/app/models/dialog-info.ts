import { Observable } from "rxjs";
import { ControlIntegral } from "./control-integral";
import { Doctores } from "./doctores";
import { Pacientes } from "./pacientes";

export interface DialogInfo {
    _listD:Doctores[],_listP:Pacientes[], _i:ControlIntegral, _type:number;
}
