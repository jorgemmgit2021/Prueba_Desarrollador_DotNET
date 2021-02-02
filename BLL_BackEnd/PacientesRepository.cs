using BLL_BackEnd.Models;
using BLL_BackEnd.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westwind.BusinessObjects;
using Westwind.Utilities;

namespace BLL_BackEnd{
    public class PacientesRepository : EntityFrameworkRepository<PruebaContext, Pacientes>
    {
        public PacientesRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Pacientes entity){
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Pacientes.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Pacientes Id</param>
        /// <returns></returns>
        public override async Task<Pacientes> Load(object PacientesId){
            Pacientes Pacientes = null;
            try{
                int id = (int)PacientesId;
                Pacientes = await Context.Pacientes
                    .Include(pct => pct.Control_Integral)
                    .FirstOrDefaultAsync(pct => pct.Id_Paciente == id);
                if (Pacientes != null){
                    Pacientes.Control_Integral = Pacientes.Control_Integral?? new List<Control_Integral>();
                    OnAfterLoaded(Pacientes);
                }
            }
            catch (InvalidOperationException){
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Pacientes - invalid Pacientes id specified.");
                return null;
            }
            catch (Exception ex){
                // handles Sql errors
                SetError(ex);
            }
            return Pacientes;
        }

        public async Task<List<Pacientes>> GetAllPacientess(int page = 0, int pageSize = 15)
        {
            IQueryable<Pacientes> Pacientess = Context.Pacientes
                .Include(ctx => ctx.Id_Paciente)
                .OrderBy(alb => alb.Nombre_Completo);

            if (page > 0)
            {
                Pacientess = Pacientess
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }

            return await Pacientess.ToListAsync();
        }

        /// <summary>
        /// This code is rather complex as EF7 can't work out
        /// the related entity updates for artist and tracks, 
        /// so this code manually  updates artists and tracks 
        /// from the saved entity using code.
        /// </summary>
        /// <param name="postedPacientes"></param>
        /// <returns></returns>
        public async Task<Pacientes> SavePacientes(Pacientes postedPacientes){
            int id = postedPacientes.Id_Paciente;
            Pacientes Pacientes;
            Pacientes = await Load(id);
            if (id < 1){
                Pacientes = Create();
                Pacientes.Control_Integral = new List<Control_Integral>();
                DataUtils.CopyObjectData(postedPacientes, Pacientes, "Control_Integral");
                postedPacientes.Control_Integral.ForEach((p) => { var c = Create<Control_Integral>(); if (p.Id_Seguimiento == 0) { c.Id_Paciente = p.Id_Paciente; c.Id_Doctor = p.Id_Doctor; c.Fecha = p.Fecha; c.Estado = p.Estado; Pacientes.Control_Integral.Add(c); } });
            }
            else{
                DataUtils.CopyObjectData(postedPacientes, Pacientes, "Control_Integral");
                postedPacientes.Control_Integral.ForEach((p) => {
                    Control_Integral c;
                    if (p.Id_Seguimiento == 0){
                        c = Create<Control_Integral>();
                        c.Id_Paciente = p.Id_Paciente; c.Id_Doctor = p.Id_Doctor; c.Fecha = p.Fecha; c.Estado = p.Estado; Pacientes.Control_Integral.Add(c);
                    }
                    else{
                        c = Pacientes.Control_Integral.FirstOrDefault(i => i.Id_Seguimiento == p.Id_Seguimiento) ?? new Control_Integral();
                        DataUtils.CopyObjectData(p, c);
                    }
                });
            }
            //now lets save it all
            if (!await SaveAsync()) return null;
            else
                return Pacientes;
        }

        public async Task<bool> DeletePacientes(int id, bool noSaveChanges = false)
        {
            //var Pacientes = await Context.Pacientes
            //    .Include(a => a.Control_Integral)
            //    .FirstOrDefaultAsync(a => a.IdPaciente == id);

            //if (Pacientes == null)
            //{
            //    SetError("Invalid Pacientes id.");
            //    return false;
            //}

            //// explicitly have to remove tracks
            //var Control_Integral = Pacientes.Control_Integral.ToList();
            //foreach (var control in Control_Integral)
            //{
            //    //for (int i = tracks.Count - 1; i > -1; i--)
            //    //{
            //    //    var track = tracks[i];
            //    Pacientes.Control_Integral.Remove(control);
            //    Context.Control.Remove(control);
            //}

            //Context.Pacientes.Remove(Pacientes);


            //if (!noSaveChanges)
            //{
            //    var result = await SaveAsync();

            //    return result;
            //}

            //return true;
            return false;
        }

        protected override bool OnValidate(Pacientes entity){
            if (entity == null)
            {
                ValidationErrors.Add("No item was passed.");
                return false;
            }

            if (string.IsNullOrEmpty(entity.Nombre_Completo))
                ValidationErrors.Add("Nombre de usuario es requerido.", "Nombre_Completo");
            else if (entity.Numero_SeguroSocial.Length<6 || string.IsNullOrEmpty(entity.Numero_SeguroSocial))
                ValidationErrors.Add("Longitud del número de seguro social incorrecta");
            return ValidationErrors.Count < 1;
        }

    }
}
