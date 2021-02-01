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
    [ServiceFilter(typeof(ApiExceptionFilter))]    
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        PruebaContext Context;
        PacientesRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<PacientesController> Logger;

        private IWebHostEnvironment HostingEnv;


        public PacientesController(PruebaContext context, PacientesRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<PacientesController> logger,
            IWebHostEnvironment env
            ){
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        // GET: api/<PacientesController>
        [HttpGet]
        public async Task<List<Pacientes>> Get(){
            return await Context.Pacientes.OrderBy(p => p.Nombre_Completo).Include(p=>p.Control_Integral).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<List<Pacientes>> GetBy(int id){
            var bhu = Context.Pacientes.Where(i => i.Control_Integral.Where(d => d.Id_Doctor == id).Count() > 0).OrderBy(e => e.Nombre_Completo).ToList();
            return await Context.Pacientes.Where(t => t.Control_Integral.Where(i => i.Id_Doctor == id).Count() > 0).OrderBy(p => p.Nombre_Completo).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Pacientes> Get(int id)
        {
            return await Context.Pacientes.Include(p=>p.Control_Integral).FirstOrDefaultAsync(p=>p.Id_Paciente == id);
        }

        // POST api/<PacientesController>
        //[HttpPost]
        //public async Task<Pacientes> Post([FromBody] int id){
        //    return await Context.Pacientes.FirstOrDefaultAsync(p => p.Id_Paciente == id);
        //}

        [HttpPost]
        public async Task<Pacientes> SavePacientes([FromBody] Pacientes paciente){
            if (!ModelState.IsValid)
                throw new ApiException("Model binding failed.", 500);

            if (!Repository.Validate(paciente))
                throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);

            var album = await Repository.SavePacientes(paciente);
            if (album == null)
                throw new ApiException(Repository.ErrorMessage, 500);

            return album;
        }

        // PUT api/<PacientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PacientesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id){
            return await Repository.DeletePacientes(id);
        }

    }
}
