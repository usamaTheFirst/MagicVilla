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
            this._response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration =30)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                _logger.Log("Getting all the villas", "");
                IEnumerable<VillaNumber> villaList = await _dbVillaNumber.GetAllAsync(includeProperties: "Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaList);
                _response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{VillaNumber:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetVillaNumber(int? VillaNumber)
        {
            try
            {

                if (VillaNumber == 0)
                {
                    _logger.Log("Get Villa error with Id " + VillaNumber, "error");
                    return BadRequest();
                }
                var villa = await _dbVillaNumber.GetAsync(u => u.VillaNo == VillaNumber, includeProperties: "Villa");

                if (villa == null)
                {

                    return NotFound();
                }


                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
                _response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createVillaDTO)
        {
            try
            {
                if (createVillaDTO == null)
                {
                    return BadRequest(createVillaDTO);
                }
                if(await _dbVilla.GetAsync(u=>u.Id == createVillaDTO.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Invalid Villa ID");

                }
                if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createVillaDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Number already exists");
                    return BadRequest(ModelState);

                }
                VillaNumber villa = _mapper.Map<VillaNumber>(createVillaDTO);
                villa.CreatedDate = DateTime.Now;

                await _dbVillaNumber.CreateAsync(villa);
                await _dbVillaNumber.Save();
                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
                _response.StatusCode = System.Net.HttpStatusCode.Created;

                return CreatedAtRoute("GetVillaNumber", new { VillaNumber = villa.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int? id)
        {
            try
            {

                if (id == 0)
                {

                    return BadRequest();
                }
                var villa = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);

                if (villa == null)
                {

                    return NotFound();
                }
                await _dbVillaNumber.RemoveAsync(villa);
                await _dbVillaNumber.Save();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaNumberUpdateDTO villaUpdateDTO)
        {
            try
            {
                if (villaUpdateDTO == null || id != villaUpdateDTO.VillaNo)
                {
                    return BadRequest();
                }
                if (await _dbVilla.GetAsync(u => u.Id == villaUpdateDTO.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Invalid Villa ID");

                }

                VillaNumber model = _mapper.Map<VillaNumber>(villaUpdateDTO);
                model.UpdatedDate = DateTime.Now;

                await _dbVillaNumber.UpdateAsync(model);
                await _dbVillaNumber.Save();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPatch("{id:int}", Name = "UpdatePartialVillaNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaNumberUpdateDTO> jsonPatchDocument)
        {
            if (jsonPatchDocument == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVillaNumber.GetAsync(u => u.VillaNo == id, track: false);
            VillaNumberUpdateDTO villaDTO = _mapper.Map<VillaNumberUpdateDTO>(villa);

            if (villa == null)
            {
                return BadRequest();
            }
            jsonPatchDocument.ApplyTo(villaDTO, ModelState);
            VillaNumber model = _mapper.Map<VillaNumber>(villaDTO);
            await _dbVillaNumber.UpdateAsync(model);
            await _dbVillaNumber.Save();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
