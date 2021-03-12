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
    public class ClientesRepository : EntityFrameworkRepository<PruebaContext, Clientes>
    {
        public ClientesRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Clientes entity){
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Clientes.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Clientes Id</param>
        /// <returns></returns>
        public override async Task<Clientes> Load(object ClientesId){
            Clientes Clientes = null;
            try{
                int id = (int)ClientesId;
                Clientes = await Context.Clientes.FirstOrDefaultAsync(dct => dct.IdCliente == id);
                if (Clientes != null){
                    OnAfterLoaded(Clientes);
                }
            }
            catch (InvalidOperationException){
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Clientes - invalid Clientes id specified.");
                return null;
            }
            catch (Exception ex){
                // handles Sql errors
                SetError(ex);
            }
            return Clientes;
        }

        public async Task<List<Clientes>> GetClientesBy(int Edad, DateTime fchIni, DateTime fchFnl){
            IQueryable<Clientes> Clientes = Context.Clientes                
                .Where(c => c.FechaNacimiento.AddYears(Edad).Year == DateTime.Today.Year)
                .Join(Context.Movimientos.Where(v=>v.Fecha>=fchIni&&v.Fecha<=fchFnl),
                c => new { IdCliente=c.IdCliente },
                m=> new { IdCliente=m.IdCliente  },
                (c, m) => new Clientes { IdCliente=c.IdCliente, NumeroIdentificacion=c.NumeroIdentificacion, 
                TipoIdentificacion=c.TipoIdentificacion, NombreCompleto=c.NombreCompleto, FechaNacimiento=c.FechaNacimiento });
            return await Clientes.Distinct<Clientes>().ToListAsync();
        }

        public List<dynamic> GetDateOfPurchase(){
            List<dynamic> _clienteFch = new List<dynamic>();
            List<dynamic>  _clientes = Context.Clientes.Join(Context.Movimientos,
            c => new { IdCliente = c.IdCliente  },
            m=> new { IdCliente = m.IdCliente },
            (c, m) => new { IdCliente=c.IdCliente, Nombre_Completo=c.NombreCompleto, Identificacion=c.NumeroIdentificacion, FechaRegistro=Context.Movimientos.Where(v=>v.IdCliente==c.IdCliente).Max(l=>l.Fecha), FechaAproximacion=DateTime.Today }).ToList<dynamic>();
            _clientes.ForEach(t => {
                int _id = t.IdCliente;
                string _Nom = t.Nombre_Completo;
                int _Idnt = t.Identificacion;
                var fchMax = Context.Movimientos.Where(n => n.IdCliente == _id).Max(h => h.Fecha);
                var fchMin = Context.Movimientos.Where(n => n.IdCliente == _id).Min(h => h.Fecha);
                var fchCnt = Context.Movimientos.Where(n => n.IdCliente == _id).Count();
                var valPrm = Context.Movimientos.Where(n => n.IdCliente == _id).Sum(m => m.Total) / fchCnt;
                var nw = new { IdCliente=_id, NombreCompleto = _Nom, Numero_Identificacion = _Idnt, FechaRegistro=fchMax, FechaAproximacion= fchMax.AddDays(fchMax.Subtract(fchMin).TotalDays / fchCnt), AproximadoVenta=valPrm};
                _clienteFch.Add(nw);
            });
            return _clienteFch.Distinct().ToList();
        }

        public async Task<List<Clientes>> GetAllClientes(int page = 0, int pageSize = 15){
            IQueryable<Clientes> Clientess = Context.Clientes
                .Include(ctx => ctx.IdCliente)
                .OrderBy(alb => alb.NombreCompleto);
            if (page > 0){
                Clientess = Clientess
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }
            return await Clientess.ToListAsync();
        }

        /// <summary>
        /// This code is rather complex as EF7 can't work out
        /// the related entity updates for artist and tracks, 
        /// so this code manually  updates artists and tracks 
        /// from the saved entity using code.
        /// </summary>
        /// <param name="postedClientes"></param>
        /// <returns></returns>
        public async Task<Clientes> SaveClientes(Clientes postedClientes){
            int id = postedClientes.IdCliente;
            Clientes Clientes;
            Clientes = await Load(id);
            if (id < 1){
                Clientes = Create();
                //DataUtils.CopyObjectData(postedClientes, Clientes, "Control_Integral");
                //postedClientes.Control_Integral.ForEach((p) => { var c = Create<Control_Integral>(); if (p.Id_Seguimiento == 0) { c.Id_Paciente = p.Id_Paciente; c.Id_Doctor = p.Id_Doctor; c.Fecha = p.Fecha; c.Estado = p.Estado; Clientes.Control_Integral.Add(c); } });
            }
            //else{
            //    DataUtils.CopyObjectData(postedClientes, Clientes, "Control_Integral");
            //    postedClientes.Control_Integral.ForEach((p) => {
            //        Control_Integral c;
            //        if (p.Id_Seguimiento == 0){
            //            c = Create<Control_Integral>();
            //            c.Id_Paciente = p.Id_Paciente; c.Id_Doctor = p.Id_Doctor; c.Fecha = p.Fecha; c.Estado = p.Estado; Clientes.Control_Integral.Add(c);
            //        }
            //        else{
            //            c = Clientes.Control_Integral.FirstOrDefault(i => i.Id_Seguimiento == p.Id_Seguimiento) ?? new Control_Integral();
            //            DataUtils.CopyObjectData(p, c);
            //        }
            //    });
            //}
            //now lets save it all
            if (!await SaveAsync()) return null;
            else
                return Clientes;
        }

        public async Task<bool> DeleteClientes(int id, bool noSaveChanges = false){
            //var Clientes = await Context.Clientes
            //    .Include(a => a.Control_Integral)
            //    .FirstOrDefaultAsync(a => a.IdPaciente == id);

            //if (Clientes == null)
            //{
            //    SetError("Invalid Clientes id.");
            //    return false;
            //}

            //// explicitly have to remove tracks
            //var Control_Integral = Clientes.Control_Integral.ToList();
            //foreach (var control in Control_Integral)
            //{
            //    //for (int i = tracks.Count - 1; i > -1; i--)
            //    //{
            //    //    var track = tracks[i];
            //    Clientes.Control_Integral.Remove(control);
            //    Context.Control.Remove(control);
            //}

            //Context.Clientes.Remove(Clientes);


            //if (!noSaveChanges)
            //{
            //    var result = await SaveAsync();

            //    return result;
            //}

            //return true;
            return false;
        }

        protected override bool OnValidate(Clientes entity){
            if (entity == null){
                ValidationErrors.Add("No item was passed.");
                return false;
            }
            if (string.IsNullOrEmpty(entity.NombreCompleto))
                ValidationErrors.Add("Nombre del cliente es requerido", "Nombre");
            else if (entity.NumeroIdentificacion < 99999 || entity.NumeroIdentificacion > 99999999)
                ValidationErrors.Add("Longitud del numero de documento incorrecto");
            return ValidationErrors.Count < 1;
        }
    }
}