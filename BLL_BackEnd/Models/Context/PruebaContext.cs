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

        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<Doctores> Doctores { get; set; }
        public DbSet<Control_Integral> Control_Integral { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
        }
            
    }
}
