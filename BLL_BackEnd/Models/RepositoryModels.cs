using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BLL_BackEnd.Models{
    public class Catalogos{
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Elemento")]
        [JsonPropertyName("Id_Elemento")]
        public int IdElemento { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Grupo")]
        [JsonPropertyName("Id_Grupo")]
        public int IdGrupo { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("Descripcion")]
        [JsonPropertyName("Descripcion")]
        public string Descripcion { get; set; }
    }

    public class Clientes{
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Cliente")]
        [JsonPropertyName("Id_Cliente")]
        public int IdCliente { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Numero_identificacion")]
        [JsonPropertyName("Numero_identificacion")]
        public int NumeroIdentificacion { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Tipo_Identificacion")]
        [JsonPropertyName("Tipo_Identificacion")]
        public int TipoIdentificacion { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Nombre_completo")]
        [JsonPropertyName("Nombre_completo")]
        public string NombreCompleto { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Fecha_nacimiento")]
        [JsonPropertyName("Fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }

    public class Inventarios{
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Item")]        
        [JsonPropertyName("Id_Item")]
        public int IdItem { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Codigo_item")]
        [JsonPropertyName("Codigo_item")]
        public int CodigoItem { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Descripcion")]
        [JsonPropertyName("Descripcion")]
        public string Descripcion { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Cantidad_stock")]
        [JsonPropertyName("Cantidad_stock")]
        public int CantidadStock { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Stock_minimo")]
        [JsonPropertyName("Stock_minimo")]
        public int StockMinimo { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Precio", TypeName = "System.Data.SqlTypes.SqlDouble")]
        [JsonPropertyName("Precio")]
        public Double Precio { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Fecha_actualizacion")]
        [JsonPropertyName("Fecha_actualizacion")]
        public DateTime FechaActualizacion { get; set; }
    }

    public class Movimientos {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Movimiento")]
        [JsonPropertyName("Id_Movimiento")]
        public int Id_Movimiento { get; set; }

        [ForeignKey("Id_Cliente")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Cliente")]
        [JsonPropertyName("Id_Cliente")]
        public int IdCliente { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Fecha")]
        [JsonPropertyName("Fecha")]
        public DateTime Fecha { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Total", TypeName = "System.Data.SqlTypes.SqlDouble")]
        [JsonPropertyName("Total")]
        public Double Total { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Estado")]
        [JsonPropertyName("Estado")]
        public bool Estado { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Tipo_movimiento")]
        [ForeignKey("Tipo_movimiento")]
        [JsonPropertyName("Tipo_movimiento")]
        public int TipoMovimiento { get; set; }

        [JsonPropertyName("Detalle_Movimiento")]
        [ForeignKey("Id_Movimiento")]
        public List<Detalle_Movimientos> Detalle_Movimientos { get; set;}
    }

    public class Detalle_Movimientos{
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Detalle")]
        [JsonPropertyName("Id_Detalle")]
        public int IdDetalle { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Movimiento")]
        [ForeignKey("Id_Movimiento")]
        [JsonPropertyName("Id_Movimiento")]
        public int Id_Movimiento { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Item")]
        [ForeignKey("Id_Item")]
        [JsonPropertyName("Id_Item")]
        public int IdItem { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Cantidad")]
        [JsonPropertyName("Cantidad")]
        public int Cantidad { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Estado")]
        [ForeignKey("Estado")]
        [JsonPropertyName("Estado")]
        public bool Estado { get; set; }
    }

    public class Venta { 
        [JsonPropertyName("cliente")]
        public Clientes Clientes { get; set; }
        [JsonPropertyName("movimiento")]
        public Movimientos Movimientos { get; set; }
    }
}
