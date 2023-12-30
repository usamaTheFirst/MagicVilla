using Asp.Versioning;
using AutoMapper;
using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Logging;
using MagicVilla.MagicApi.Model;
using MagicVilla.MagicApi.Model.DTO;
using MagicVilla.MagicApi.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla.MagicApi.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;

        public VillaNumberAPIController(ILogging logger, IVillaNumberRepository db, IMapper mapper, IVillaRepository dbVilla)
        {
            _logger = logger;
            _dbVillaNumber = db;
            _dbVilla = dbVilla;
            _mapper = mapper;
            _response = new();
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
