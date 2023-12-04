using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Logging;
using MagicVilla.MagicApi.Model;
using MagicVilla.MagicApi.Model.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.MagicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ILogging logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all the villas","");
            return Ok( _db.Villas);
        }
        [HttpGet("{id:int}",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDTO> GetVilla(int? id)
        {
  
            if (id ==0)
            {
                _logger.Log("Get Villa error with Id " + id, "error" );
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {

                return NotFound();
            }


            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(_db.Villas.FirstOrDefault(u=>u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa already exists");
                return BadRequest(ModelState);

            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa model = new Villa
            {
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Sqft = villaDTO.Sqft,
                Amenities = villaDTO.Amenities,
                ImageURL = villaDTO.ImageURL,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate
            };
            _db.Villas.AddAsync(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id =villaDTO.Id  },villaDTO);
        }
        [HttpDelete("{id:int}",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteVilla(int? id)
        {


            if (id == 0)
            {

                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {

                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            //var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Occupancy = villaDTO.Occupancy;
            //villa.Sqft = villaDTO.Sqft;
            Villa model = new Villa
            {
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Sqft = villaDTO.Sqft,
                Id = villaDTO.Id,
                Amenities = villaDTO.Amenities,
                ImageURL = villaDTO.ImageURL,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();

        }
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
      public  IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> jsonPatchDocument)
        {
            if(jsonPatchDocument == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            VillaDTO villaDTO    = new VillaDTO
            {
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Sqft = villa.Sqft,
                Id = villa.Id,
                Amenities = villa.Amenities,
                ImageURL = villa.ImageURL,
                Details = villa.Details,
                Rate = villa.Rate
            };
            if (villa == null)
            {
                return BadRequest();
            }
            jsonPatchDocument.ApplyTo(villaDTO, ModelState);
            Villa model = new Villa
            {
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Sqft = villaDTO.Sqft,
                Id = villaDTO.Id,
                Amenities = villaDTO.Amenities,
                ImageURL = villaDTO.ImageURL,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
