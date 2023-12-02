using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Model;
using MagicVilla.MagicApi.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla.MagicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.VillaList;
        }
        [HttpGet("{id:int}")]
        public HttpResponseMessage GetVilla(int? id)
        {
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                var ErrorResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
                ErrorResponse.Content = new StringContent("Villa not found.");
                return ErrorResponse;
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(villa.ToString());
            return response;
        }
    }
}
