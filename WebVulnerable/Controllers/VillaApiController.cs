using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebVulnerable.Data;
using WebVulnerable.Data.Dto;

namespace WebVulnerable.Controllers
{
    [Route("api/villa")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVilla()
        {
            return Ok(VillaStore.villaList);

        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//provide documented in swagger
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if (villa == null) {
                return NotFound();
            }
            return Ok(villa);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody]VillaDto villaDto)
        {
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaStore.villaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1;
            VillaStore.villaList.Add(villaDto);

            return CreatedAtRoute(nameof(GetVilla), new {id = villaDto.Id } ,villaDto); //add location header, create 201 response code
        }

        [HttpDelete("{id}", Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
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
            VillaStore.villaList.Remove(villa);
            return Ok();
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}", Name = "UpdateVilla")] //Use put to update all field
        public IActionResult UpdateVilla(int id, [FromBody]VillaDto villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u=>u.Id == id);
            villa.Name = villaDTO.Name;
            villa.Occupancy = villaDTO.Occupancy;
            villa.Sqft = villaDTO.Sqft;

            return Ok();
        }

        //[HttpPatch("{id}", Name = "UpdatePartialVilla")]
        //public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        //{
        //    if (patchDto == null || id ==0)
        //    {
        //        return BadRequest();
        //    }
        //    var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

        //    if (villa == null)
        //    {
        //        return BadRequest();
        //    }

        //    patchDto.ApplyTo(villa, ModelState);
        //    return Ok();
        //}



    }
}
