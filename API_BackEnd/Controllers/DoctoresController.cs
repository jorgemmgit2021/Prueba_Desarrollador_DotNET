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
    public class DoctoresController : ControllerBase
    {
        PruebaContext Context;
        DoctoresRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<DoctoresController> Logger;

        private IWebHostEnvironment HostingEnv;

        public DoctoresController(PruebaContext context, DoctoresRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<DoctoresController> logger,
            IWebHostEnvironment env
            ) {
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        // GET: api/<DoctoresController>
        [HttpGet]
        public async Task<List<Doctores>> Get()
        {
            return await Context.Doctores.OrderBy(d => d.Nombre_Completo).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<List<Doctores>> GetBy(int id){
            return await Context.Doctores.Where(t => t.Control_Integral.Where(i=>i.Id_Paciente==id).Count()>0).OrderBy(d=>d.Nombre_Completo).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Doctores> Get(int id){
            return await Context.Doctores.Include(c=>c.Control_Integral).FirstOrDefaultAsync(p => p.Id_Doctor == id);
        }

        // POST api/<DoctoresController>
        //[HttpPost]
        //public async Task<Doctores> Post([FromBody] int id){
        //    return await Context.Doctores.FirstOrDefaultAsync(p => p.Id_Paciente == id);
        //}

        [HttpPost]
        public async Task<Doctores> SaveDoctores([FromBody] Doctores doctor)
        {
            if (!ModelState.IsValid)
                throw new ApiException("Model binding failed.", 500);

            if (!Repository.Validate(doctor))
                throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);

            var album = await Repository.SaveDoctores(doctor);
            if (album == null)
                throw new ApiException(Repository.ErrorMessage, 500);

            return album;
        }

        // PUT api/<DoctoresController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctoresController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id){
            return await Repository.DeleteDoctores(id);
        }

    }
}
