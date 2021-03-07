import { DetalleMovimientos } from "./detalle-movimientos";

export interface Movimientos {
    Id_Movimiento:number;
    Id_Cliente:number;
    Fecha:string;
    Total:string;
    Estado:boolean;
    Tipo_movimiento:number;
    Detalle_Movimiento:DetalleMovimientos[]
}
