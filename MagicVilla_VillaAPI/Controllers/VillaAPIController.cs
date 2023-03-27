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



        ///* START :- First Implementation without ActionResult */

        ////Get one Villa by Id
        ////[HttpGet("id")]
        //[HttpGet("{id:int}")]
        //public VillaDTO GetVilla(int id)
        //{

        //    return VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        //}

        ///* END :- First Implementation without ActionResult */
        ///



        /* START :- Implementation with ActionResult */
        
        [HttpGet("{id:int}")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        /* END :- Implementation with ActionResult */
    }
}
