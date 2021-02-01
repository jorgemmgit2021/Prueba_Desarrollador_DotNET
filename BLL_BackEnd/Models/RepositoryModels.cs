using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BLL_BackEnd.Models
{
    [DebuggerDisplay("{Paciente} {Pacientes.Nombre_Completo}")]
    public class Pacientes{
        [Key]
        [JsonPropertyName("Id_Paciente")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Paciente")]
        public int Id_Paciente { get; set; }

        [JsonPropertyName("Nombre_Completo")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Nombre_Completo")]
        public string Nombre_Completo { get; set; }

        [JsonPropertyName("Numero_Seguro_Social")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Numero_Seguro_Social")]
        public string Numero_SeguroSocial { get; set; }

        [JsonPropertyName("Codigo_Postal")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Codigo_Postal")]
        public string Codigo_Postal { get; set; }

        [JsonPropertyName("Telefono_Contacto")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Telefono_Contacto")]
        public string Telefono_Contacto { get; set; }

        //public int Id_Seguimiento { get; set; }

        [JsonPropertyName("Control_Integral")]
        [ForeignKey("Id_Paciente")]
        public List<Control_Integral> Control_Integral { get; set; }
    }

    public class Doctores{
        [Key]
        [JsonPropertyName("Id_Doctor")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Doctor")]
        public int Id_Doctor { get; set; }

        [JsonPropertyName("Nombre_Completo")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Nombre_Completo")]
        public string Nombre_Completo { get; set; }

        [JsonPropertyName("Especialidad")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Especialidad")]
        public string Especialidad { get; set; }

        [JsonPropertyName("Numero_Credencial")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Numero_Credencial")]
        public decimal Numero_Credencial { get; set; }

        [JsonPropertyName("Hospital_Adscrito")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Hospital_Adscrito")]
        public string Hospital_Adscrito { get; set; }
        [JsonPropertyName("Control_Integral")]
        [ForeignKey("Id_Doctor")]
        public List<Control_Integral> Control_Integral { get; set; }
    }

    public class Control_Integral{
        [Key]
        [JsonPropertyName("Id_Seguimiento")]
        public int? Id_Seguimiento { get; set; }

        [JsonPropertyName("Id_Paciente")]
        [ForeignKey("Id_Paciente")]
        public int Id_Paciente { get; set; }

        [JsonPropertyName("Id_Doctor")]
        [ForeignKey("Id_Doctor")]
        public int Id_Doctor { get; set; }

        [JsonPropertyName("Fecha")]
        public DateTime Fecha { get; set; }

        [JsonPropertyName("Estado")]
        public bool Estado { get; set; }
    }
}
