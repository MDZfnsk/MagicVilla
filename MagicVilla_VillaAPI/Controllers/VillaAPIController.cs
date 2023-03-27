using MagicVilla_VillaAPI.Custom_Logger;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        ///*** START :- Using inbuild logger ***/
        //private readonly ILogger<VillaAPIController> _logger;

        ////Use dependancy injection to access logger
        //public VillaAPIController(ILogger<VillaAPIController> logger)
        //{
        //    _logger = logger;
        //}
        ///*** END :- Using inbuild logger ***/



        /*** START :- Using Custom logger ***/
        private readonly ILogging _logger;
        public VillaAPIController(ILogging logger)
        {
            _logger = logger;
           
        }
        /*** END :- Using Custom logger ***/



        /*** ****** HTTP GET ******** ***/

        ///* START :- Without using Action Result */
        ////Get all Villas
        //[HttpGet]
        //public IEnumerable<VillaDTO> GetVillas() 
        //{

        //    return VillaStore.villaList;
        //}
        ///* END :- Without using Action Result */


        /* START :- Using Action Result */
        //Get all Villas
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            ////using inbuilt logger
            //_logger.LogInformation("Getting all Villas");

            //using custom logger
            _logger.Log("Getting all Villas","");

            return VillaStore.villaList;
        }
        /* END :- Using Action Result */





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



        ///* START :- Implementation with ActionResult */

        //[HttpGet("{id:int}")]
        //public ActionResult<VillaDTO> GetVilla(int id)
        //{
        //    if(id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

        //    if(villa == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(villa);
        //}

        ///* END :- Implementation with ActionResult */



        ///* START :- With ResponseType Documentations  */
        ////Return Type not mentioning at the function name level

        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //[HttpGet("{id:int}")]
        //public ActionResult GetVilla(int id)
        //{
        //    if (id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

        //    if (villa == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(villa);
        //}

        ///* END :- With ResponseType Documentations */


        /* START :- With ResponseType Documentations using StatusCde  */

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[HttpGet("{/*id:int}")]
        [HttpGet("{id:int}", Name = "GetVilla")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
           
            if (id == 0)
            {
                ////using inbuilt logger
                //_logger.LogError("Get Villa Error with Id" + id);

                //using custom logger
                _logger.Log("Get Villa Error with Id" + id,"error");

                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        /* END :- With ResponseType Documentations using StatusCde  */



        /*** ****** /  HTTP GET ******** ***/



        /*** ******  HTTP POST ******** ***/

        ///* START:- Return the created Object as the Response */

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]

        //public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        //{
        //    if (villaDTO == null)
        //    {
        //        return BadRequest(villaDTO);
        //    }
        //    if (villaDTO.Id > 0)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //    villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        //    VillaStore.villaList.Add(villaDTO);

        //    return Ok(villaDTO);
        //}

        ///* END:- Return the created Object as the Response */


        ///* START:- Return the URl of created Object with the Response Header */

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]

        //public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        //{
        //    ////When using without [ApiController]
        //    ////**Comment Out [ApiController] At the very begining of this page to use the following
        //    ////To Validate
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}

        //    if (villaDTO == null)
        //    {
        //        return BadRequest(villaDTO);
        //    }
        //    if (villaDTO.Id > 0)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //    villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        //    VillaStore.villaList.Add(villaDTO);

        //    return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        //}

        ///* END:- Return the URl of created Object with the Response Header */


        /* START:- Check whether the name already exisits */

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (VillaStore.villaList.FirstOrDefault(u=>u.Name.ToLower() == villaDTO.Name.ToLower()) != null) 
            {
                //first parameter is the error name, second is the message
                ModelState.AddModelError("CustomError", "Villa Already Exists !");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        /* END :- Check whether the name already exisits */

        /*** ****** / HTTP POST ******** ***/



        /*** ****** HTTP DELETE ******** ***/

        [HttpDelete("{id:int}",Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult DeleteVilla(int id)
        {
            if( id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        /*** ****** / HTTP DELETE ******** ***/





        /*** ****** HTTP PUT ******** ***/

        [HttpPut("{id:int}",Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if( villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent();
        }
        /*** ****** / HTTP PUT ******** ***/



        /*** ****** HTTP PATCH ******** ***/

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }
        /*** ****** / HTTP PATCH ******** ***/

    }
}
