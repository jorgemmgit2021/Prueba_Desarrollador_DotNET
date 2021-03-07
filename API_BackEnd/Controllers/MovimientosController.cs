using API_BackEnd.API;
using BLL_BackEnd;
using BLL_BackEnd.Models;
using BLL_BackEnd.Models.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase{
        PruebaContext Context;
        MovimientosRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<MovimientosController> Logger;

        private IWebHostEnvironment HostingEnv;

        public MovimientosController(PruebaContext context, MovimientosRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<MovimientosController> logger,
            IWebHostEnvironment env
            ){
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        // GET: api/<MovimientosController>
        [HttpGet]
        public async Task<List<Movimientos>> Get(){
            return await Context.Movimientos.Include(m=>m.Detalle_Movimientos.OrderBy(v => v.IdItem)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Movimientos> Get(int id){
            return await Context.Movimientos.Include(m=>m.Detalle_Movimientos).FirstOrDefaultAsync(v => v.Id_Movimiento == id);
        }

        //[HttpPost]
        //public async Task<Movimientos> SaveMovimientos([FromBody] Movimientos movimientos){
        //    if (!ModelState.IsValid)
        //        throw new ApiException("Model binding failed.", 500);

        //    if (!Repository.Validate(movimientos))
        //        throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);

        //    var result = await Repository.SaveMovimientos(movimientos);
        //    if (result == null)
        //        throw new ApiException(Repository.ErrorMessage, 500);

        //    return result;
        //}

        [HttpPost]
        public async Task<Venta> RegistrarVenta([FromBody] Venta venta){            
            return await Repository.SaveVenta(venta);
        }

        // PUT api/<MovimientosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovimientosController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id){
            return await Repository.DeleteMovimientos(id);
        }

    }
}
