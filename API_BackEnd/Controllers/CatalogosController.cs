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
    public class CatalogosController : ControllerBase{
        PruebaContext Context;
        CatalogosRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<CatalogosController> Logger;

        private IWebHostEnvironment HostingEnv;

        public CatalogosController(PruebaContext context, CatalogosRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<CatalogosController> logger,
            IWebHostEnvironment env
            ){
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        [HttpGet("{id}")]
        public async Task<List<Catalogos>> Get(int id){
            return await Context.Catalogos.Where(c =>c.IdGrupo == id).ToListAsync();
        }
    }
}
