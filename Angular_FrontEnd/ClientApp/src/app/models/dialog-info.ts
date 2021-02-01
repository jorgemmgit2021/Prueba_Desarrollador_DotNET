import { Observable } from "rxjs";
import { ControlIntegral } from "./control-integral";
import { Doctores } from "./doctores";

export interface DialogInfo {
    _list:Observable<Doctores[]>, _i:ControlIntegral, _type:number;
}
