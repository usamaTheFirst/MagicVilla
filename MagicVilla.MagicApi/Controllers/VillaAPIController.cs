﻿using AutoMapper;
using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Logging;
using MagicVilla.MagicApi.Model;
using MagicVilla.MagicApi.Model.DTO;
using MagicVilla.MagicApi.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla.MagicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        private readonly IVillaRepository _dbVilla;
        public VillaAPIController(ILogging logger, IVillaRepository db, IMapper mapper)
        {
            _logger = logger;
            _dbVilla = db;
            _mapper = mapper;
            this._response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.Log("Getting all the villas", "");
                IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
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

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetVilla(int? id)
        {
            try
            {

                if (id == 0)
                {
                    _logger.Log("Get Villa error with Id " + id, "error");
                    return BadRequest();
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id);

                if (villa == null)
                {

                    return NotFound();
                }


                _response.Result = _mapper.Map<VillaDTO>(villa);
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
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createVillaDTO)
        {
            try
            {
                if (createVillaDTO == null)
                {
                    return BadRequest(createVillaDTO);
                }
                if (await _dbVilla.GetAsync(u => u.Name.ToLower() == createVillaDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("", "Villa already exists");
                    return BadRequest(ModelState);

                }
                Villa villa = _mapper.Map<Villa>(createVillaDTO);

                await _dbVilla.CreateAsync(villa);
                await _dbVilla.Save();
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = System.Net.HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int? id)
        {
            try
            {

                if (id == 0)
                {

                    return BadRequest();
                }
                var villa = await _dbVilla.GetAsync(u => u.Id == id);

                if (villa == null)
                {

                    return NotFound();
                }
                await _dbVilla.RemoveAsync(villa);
                await _dbVilla.Save();
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
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            try
            {
                if (villaUpdateDTO == null || id != villaUpdateDTO.Id)
                {
                    return BadRequest();
                }

                Villa model = _mapper.Map<Villa>(villaUpdateDTO);

                await _dbVilla.UpdateAsync(model);
                await _dbVilla.Save();
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
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> jsonPatchDocument)
        {
            if (jsonPatchDocument == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id, track: false);
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null)
            {
                return BadRequest();
            }
            jsonPatchDocument.ApplyTo(villaDTO, ModelState);
            Villa model = _mapper.Map<Villa>(villaDTO);
            //Villa model = new Villa
            //{
            //    Name = villaDTO.Name,
            //    Occupancy = villaDTO.Occupancy,
            //    Sqft = villaDTO.Sqft,
            //    Id = villaDTO.Id,
            //    Amenities = villaDTO.Amenities,
            //    ImageURL = villaDTO.ImageURL,
            //    Details = villaDTO.Details,
            //    Rate = villaDTO.Rate
            //};
            await _dbVilla.UpdateAsync(model);
            await _dbVilla.Save();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
