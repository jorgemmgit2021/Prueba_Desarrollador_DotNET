import { ɵHttpInterceptingHandler } from "@angular/common/http";
import { ControlContainer } from "@angular/forms";
import {ControlIntegral} from "./control-integral";

export interface Pacientes {
    Id_Paciente:number;
    Nombre_Completo:string;
    Numero_Seguro_Social:string;
    Codigo_Postal:string;
    Telefono_Contacto:string;
    Control_Integral:ControlIntegral[];
}
