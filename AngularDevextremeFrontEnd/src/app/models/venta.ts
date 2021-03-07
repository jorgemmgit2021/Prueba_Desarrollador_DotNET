import { Clientes } from "./clientes";
import { DetalleMovimientos } from "./detalle-movimientos";
import { Inventarios } from "./inventarios";
import { Movimientos } from "./movimientos";

export interface Venta {
    cliente:Clientes;
    inventario:Inventarios[];
    movimiento:Movimientos;    
}
