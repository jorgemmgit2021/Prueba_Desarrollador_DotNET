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
    public class ClientesController : ControllerBase{
        PruebaContext Context;
        ClientesRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<ClientesController> Logger;

        private IWebHostEnvironment HostingEnv;

        public ClientesController(PruebaContext context, ClientesRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<ClientesController> logger,
            IWebHostEnvironment env
            ){
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        // GET: api/<ClientesController>
        [HttpGet]
        public async Task<List<Clientes>> GetAll(){
            return await Context.Clientes.OrderBy(c => c.NombreCompleto).ToListAsync();
        }

        [HttpGet("Get/{identificacion}")]
        public async Task<Clientes> Get(int identificacion){
            return await Context.Clientes.FirstOrDefaultAsync(c => c.NumeroIdentificacion == identificacion);
        }

        [HttpGet("GetBy/{id}")]
        public async Task<List<Clientes>> GetBy(int Edad, DateTime fchIni, DateTime fchFnl){
            return await Repository.GetClientesBy(Edad, fchIni, fchFnl);
        }

        [HttpGet("GetDateOfPurchase")]
        public List<dynamic> GetDateOfPurchase(){
            return Repository.GetDateOfPurchase();
        }

        [HttpPost]
        public async Task<Clientes> SaveClientes([FromBody] Clientes cliente){
            if (!ModelState.IsValid)
                throw new ApiException("Model binding failed.", 500);

            if (!Repository.Validate(cliente))
                throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);

            var album = await Repository.SaveClientes(cliente);
            if (album == null)
                throw new ApiException(Repository.ErrorMessage, 500);

            return album;
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id){
            return await Repository.DeleteClientes(id);
        }

    }
}
