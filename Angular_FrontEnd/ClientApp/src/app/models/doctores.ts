import { ControlIntegral } from "./control-integral";

export interface Doctores {
    Id_Doctor:number,
    Nombre_Completo:string,
    Especialidad:string,
    Numero_Credencial:number
    Hospital_Adscrito:string
    Control_Integral:ControlIntegral[]
}