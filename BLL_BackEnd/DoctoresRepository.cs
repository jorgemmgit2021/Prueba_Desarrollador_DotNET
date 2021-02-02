using BLL_BackEnd.Models;
using BLL_BackEnd.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westwind.BusinessObjects;
using Westwind.Utilities;

namespace BLL_BackEnd
{
    public class DoctoresRepository : EntityFrameworkRepository<PruebaContext, Doctores>
    {
        public DoctoresRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Doctores entity)
        {
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Doctores.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Doctores Id</param>
        /// <returns></returns>
        public override async Task<Doctores> Load(object DoctoresId)
        {
            Doctores Doctores = null;
            try
            {
                int id = (int)DoctoresId;
                Doctores = await Context.Doctores
                    .Include(pct => pct.Control_Integral)
                    .FirstOrDefaultAsync(dct => dct.Id_Doctor == id);
                if (Doctores != null)
                {
                    Doctores.Control_Integral = Doctores.Control_Integral ?? new List<Control_Integral>();
                    OnAfterLoaded(Doctores);
                }
            }
            catch (InvalidOperationException)
            {
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Doctores - invalid Doctores id specified.");
                return null;
            }
            catch (Exception ex)
            {
                // handles Sql errors
                SetError(ex);
            }
            return Doctores;
        }

        public async Task<List<Doctores>> GetAllDoctoress(int page = 0, int pageSize = 15)
        {
            IQueryable<Doctores> Doctoress = Context.Doctores
                .Include(ctx => ctx.Id_Doctor)
                .OrderBy(alb => alb.Nombre_Completo);

            if (page > 0)
            {
                Doctoress = Doctoress
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }

            return await Doctoress.ToListAsync();
        }

        /// <summary>
        /// This code is rather complex as EF7 can't work out
        /// the related entity updates for artist and tracks, 
        /// so this code manually  updates artists and tracks 
        /// from the saved entity using code.
        /// </summary>
        /// <param name="postedDoctores"></param>
        /// <returns></returns>
        public async Task<Doctores> SaveDoctores(Doctores postedDoctores){
            int id = postedDoctores.Id_Doctor;
            Doctores Doctores;
            Doctores = await Load(id);
            if (id < 1){
                Doctores = Create();
                Doctores.Control_Integral = new List<Control_Integral>();
                DataUtils.CopyObjectData(postedDoctores, Doctores, "Control_Integral");
                postedDoctores.Control_Integral.ForEach((p) => { var c = Create<Control_Integral>(); if (p.Id_Seguimiento == 0) { c.Id_Paciente = p.Id_Paciente; c.Id_Doctor = p.Id_Doctor; c.Fecha = p.Fecha; c.Estado = p.Estado; Doctores.Control_Integral.Add(c); } });
            }
            else{
                DataUtils.CopyObjectData(postedDoctores, Doctores, "Control_Integral");
                postedDoctores.Control_Integral.ForEach((p) => { 
                    Control_Integral c;
                    if (p.Id_Seguimiento == 0){
                        c = Create<Control_Integral>();
                        c.Id_Paciente = p.Id_Paciente; c.Id_Doctor = p.Id_Doctor; c.Fecha = p.Fecha; c.Estado = p.Estado; Doctores.Control_Integral.Add(c); 
                    }
                    else {
                        c = Doctores.Control_Integral.FirstOrDefault(i => i.Id_Seguimiento == p.Id_Seguimiento)??new Control_Integral();
                        DataUtils.CopyObjectData(p, c);
                    }
                });
            }
            //now lets save it all
            if (!await SaveAsync()) return null;
            else
                return Doctores;
        }

        public async Task<bool> DeleteDoctores(int id, bool noSaveChanges = false)
        {
            //var Doctores = await Context.Doctores
            //    .Include(a => a.Control_Integral)
            //    .FirstOrDefaultAsync(a => a.IdPaciente == id);

            //if (Doctores == null)
            //{
            //    SetError("Invalid Doctores id.");
            //    return false;
            //}

            //// explicitly have to remove tracks
            //var Control_Integral = Doctores.Control_Integral.ToList();
            //foreach (var control in Control_Integral)
            //{
            //    //for (int i = tracks.Count - 1; i > -1; i--)
            //    //{
            //    //    var track = tracks[i];
            //    Doctores.Control_Integral.Remove(control);
            //    Context.Control.Remove(control);
            //}

            //Context.Doctores.Remove(Doctores);


            //if (!noSaveChanges)
            //{
            //    var result = await SaveAsync();

            //    return result;
            //}

            //return true;
            return false;
        }

        protected override bool OnValidate(Doctores entity)
        {
            if (entity == null)
            {
                ValidationErrors.Add("No item was passed.");
                return false;
            }

            if (string.IsNullOrEmpty(entity.Nombre_Completo))
                ValidationErrors.Add("Nombre del usuario es requerido", "Nombre");
            else if (entity.Numero_Credencial < 99999 || entity.Numero_Credencial> 99999999)
                ValidationErrors.Add("Longitud del numero de credencial incorrecto");
            return ValidationErrors.Count < 1;
        }

    }
}