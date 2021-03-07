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
    public class CatalogosRepository : EntityFrameworkRepository<PruebaContext, Catalogos>{
        public CatalogosRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Catalogos entity)
        {
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Catalogos.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Catalogos Id</param>
        /// <returns></returns>
        public override async Task<Catalogos> Load(object CatalogosId){
            Catalogos Catalogos = null;
            try{
                int id = (int)CatalogosId;
                Catalogos = await Context.Catalogos
                    .FirstOrDefaultAsync(ctl => ctl.IdGrupo == id);
                if (Catalogos != null){
                    OnAfterLoaded(Catalogos);
                }
            }
            catch (InvalidOperationException)
            {
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Catalogos - invalid Catalogos id specified.");
                return null;
            }
            catch (Exception ex)
            {
                // handles Sql errors
                SetError(ex);
            }
            return Catalogos;
        }
    }
}
