using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //Get all Villas
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas() 
        {

            return VillaStore.villaList;
        }


        //Get one Villa by Id
        //[HttpGet("id")]
        [HttpGet("{id:int}")]
        public VillaDTO GetVilla(int id)
        {

            return VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        }
    }
}
