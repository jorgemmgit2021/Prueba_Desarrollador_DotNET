using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_BackEnd.Models.Context
{
    public class PruebaContext:DbContext{
        public PruebaContext(DbContextOptions options):
            base(options){

        }
        public PruebaContext(){

        }

        public DbSet<Catalogos> Catalogos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Inventarios> Inventarios { get; set; }
        public DbSet<Movimientos> Movimientos { get; set; }
        public DbSet<Detalle_Movimientos> Detalle_Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
        }
            
    }
}
