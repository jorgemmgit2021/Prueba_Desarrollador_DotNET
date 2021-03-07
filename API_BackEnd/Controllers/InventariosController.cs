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
    public class InventariosController : ControllerBase{
        PruebaContext Context;
        InventariosRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<InventariosController> Logger;

        private IWebHostEnvironment HostingEnv;

        public InventariosController(PruebaContext context, InventariosRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<InventariosController> logger,
            IWebHostEnvironment env
            ){
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        // GET: api/<InventariosController>
        [HttpGet]
        public async Task<List<Inventarios>> Get(){
//.OrderBy(i => i.Descripcion)
            return await Context.Inventarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Inventarios> Get(int id){
            return await Context.Inventarios.FirstOrDefaultAsync(i => i.IdItem == id);
        }

        [HttpGet("GetAllStockMin")]
        public async Task<List<Inventarios>> GetAllStockMin(){
            return await Repository.GetAllStockMin();
        }

        [HttpGet("GetAllVentasXItem")]
        public List<dynamic> GetAllVentasXItem(int periodo){
            return Repository.GetAllVentasXItem(periodo);
        }
        
        [HttpPost]
        public async Task<Inventarios> SaveInventarios([FromBody] Inventarios cliente){
            if (!ModelState.IsValid)
                throw new ApiException("Model binding failed.", 500);

            if (!Repository.Validate(cliente))
                throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);

            var album = await Repository.SaveInventarios(cliente);
            if (album == null)
                throw new ApiException(Repository.ErrorMessage, 500);

            return album;
        }

        // PUT api/<InventariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InventariosController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id){
            return await Repository.DeleteInventarios(id);
        }

    }
}
