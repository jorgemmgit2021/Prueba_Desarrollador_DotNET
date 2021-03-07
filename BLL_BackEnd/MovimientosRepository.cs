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
    public class MovimientosRepository : EntityFrameworkRepository<PruebaContext, Movimientos>
    {
        public MovimientosRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Movimientos entity)
        {
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Movimientos.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Movimientos Id</param>
        /// <returns></returns>
        public override async Task<Movimientos> Load(object MovimientosId){
            Movimientos Movimientos = null;
            try
            {
                int id = (int)MovimientosId;
                Movimientos = await Context.Movimientos.Include(m=>m.Detalle_Movimientos)
                    .FirstOrDefaultAsync(m => m.Id_Movimiento == id);
                if (Movimientos != null){
                    Movimientos.Detalle_Movimientos = Movimientos.Detalle_Movimientos ?? new List<Detalle_Movimientos>();
                    OnAfterLoaded(Movimientos);
                }
            }
            catch (InvalidOperationException){
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Movimientos - invalid Movimientos id specified.");
                return null;
            }
            catch (Exception ex){
                // handles Sql errors
                SetError(ex);
            }
            return Movimientos;
        }

        public async Task<List<Movimientos>> GetAllMovimientoss(int page = 0, int pageSize = 15){
            IQueryable<Movimientos> Movimientoss = Context.Movimientos
                .Include(ctx => ctx.Detalle_Movimientos)
                .OrderBy(alb => alb.Fecha);

            if (page > 0){
                Movimientoss = Movimientoss
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }
            return await Movimientoss.ToListAsync();
        }

        /// <summary>
        /// This code is rather complex as EF7 can't work out
        /// the related entity updates for artist and tracks, 
        /// so this code manually  updates artists and tracks 
        /// from the saved entity using code.
        /// </summary>
        /// <param name="postedMovimientos"></param>
        /// <returns></returns>
        public async Task<Movimientos> SaveMovimientos(Movimientos postedMovimientos){
            int id = postedMovimientos.Id_Movimiento;
            Movimientos Movimientos;
            Movimientos = await Load(id);
            if (id < 1){
                Movimientos = Create();
                Movimientos.Detalle_Movimientos = new List<Detalle_Movimientos>();
                DataUtils.CopyObjectData(postedMovimientos, Movimientos, "Detalle_Movimientos");
                postedMovimientos.Detalle_Movimientos.ForEach((m) => { var d = Create<Detalle_Movimientos>(); if (m.Id_Movimiento == 0) { d.Id_Movimiento = m.Id_Movimiento; d.IdItem = m.IdItem; d.Cantidad = m.Cantidad; d.Estado = m.Estado; } Movimientos.Detalle_Movimientos.Add(d);  });
            }
            else
            {
                DataUtils.CopyObjectData(postedMovimientos, Movimientos, "Detalle_Movimientos");
                postedMovimientos.Detalle_Movimientos.ForEach((d) => {
                    Detalle_Movimientos t;
                    if (d.Id_Movimiento == 0){
                        t = Create<Detalle_Movimientos>();
                        t.Id_Movimiento = d.Id_Movimiento; t.Cantidad = d.Cantidad; t.IdItem = d.IdItem; t.Estado = d.Estado; Movimientos.Detalle_Movimientos.Add(t);
                    }
                    else{
                        t = Movimientos.Detalle_Movimientos.FirstOrDefault(i => i.IdDetalle == d.IdDetalle) ?? new Detalle_Movimientos();
                        DataUtils.CopyObjectData(t, d);
                    }
                });
            }
            //now lets save it all
            if (!await SaveAsync()) return null;
            else
                return Movimientos;
        }

        public async Task<Venta> SaveVenta(Venta postedVenta){
            ClientesRepository clientesRepository = new ClientesRepository(Context);
            Movimientos _mvt = new Movimientos();
            var awC = await clientesRepository.SaveAsync(postedVenta.Clientes);
            if (awC){
                postedVenta.Movimientos.IdCliente = postedVenta.Clientes.IdCliente;
                _mvt = await SaveMovimientos(postedVenta.Movimientos);
                postedVenta.Movimientos = _mvt;
            }
            return postedVenta;
        }

        public async Task<bool> DeleteMovimientos(int id, bool noSaveChanges = false)
        {
            //var Movimientos = await Context.Movimientos
            //    .Include(a => a.Control_Integral)
            //    .FirstOrDefaultAsync(a => a.IdPaciente == id);

            //if (Movimientos == null)
            //{
            //    SetError("Invalid Movimientos id.");
            //    return false;
            //}

            //// explicitly have to remove tracks
            //var Control_Integral = Movimientos.Control_Integral.ToList();
            //foreach (var control in Control_Integral)
            //{
            //    //for (int i = tracks.Count - 1; i > -1; i--)
            //    //{
            //    //    var track = tracks[i];
            //    Movimientos.Control_Integral.Remove(control);
            //    Context.Control.Remove(control);
            //}

            //Context.Movimientos.Remove(Movimientos);


            //if (!noSaveChanges)
            //{
            //    var result = await SaveAsync();

            //    return result;
            //}

            //return true;
            return false;
        }

        protected override bool OnValidate(Movimientos entity){
            if (entity == null){
                ValidationErrors.Add("No item was passed.");
                return false;
            }

            if (entity.Detalle_Movimientos.Count<1)
                ValidationErrors.Add("Detalles del movimiento es requerido.", "Descripcion tipo de movimiento");
            else if (entity.Total < 10000 || entity.Total>9999999)
                ValidationErrors.Add("Longitud del valor total incorrecta");
            return ValidationErrors.Count < 1;
        }
    }
}
