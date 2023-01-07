using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPI.Data;
using WebAPI.Logging;
using WebAPI.Model;
using WebAPI.Model.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }
        //private readonly ILogging _logger;
        //public VillaAPIController(ILogging logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogError("Hey!!!!!!!!!!!Error Occurred");
            return Ok(VillaStore.Villas);
        }
        [HttpGet("{Id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetVilla(int Id)
        {
            if (Id == 0)
                return BadRequest();

            var villa= VillaStore.Villas.FirstOrDefault(x => x.Id == Id);

            if (villa == null)
                return NotFound();

            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> SaveVilla([FromBody]VillaDTO villa)
        {
            // if (!ModelState.IsValid)
            //   return BadRequest(ModelState);

            if (VillaStore.Villas.ToList().Any(x => x.Name == villa.Name))
            {
                ModelState.AddModelError("CustomerError", "Villa Name already exists");
                return BadRequest(ModelState);
            }

            if (villa == null)
                return BadRequest(villa);

            if (villa.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            villa.Id = VillaStore.Villas.OrderByDescending(x => x.Id).FirstOrDefault().Id  + 1;
            VillaStore.Villas.Add(villa);

            //return Ok(villa);
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            // if (!ModelState.IsValid)
            //   return BadRequest(ModelState);

            
            if (id == 0)
                return BadRequest();

            var villa = VillaStore.Villas.FirstOrDefault(x => x.Id == id);


            if (villa == null)
                return NotFound();

            VillaStore.Villas.Remove(villa);

            //return Ok(villa);
            return NoContent();
        }
        [HttpPut("{id:int}", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            // if (!ModelState.IsValid)
            //   return BadRequest(ModelState);


            if (id == 0)
                return BadRequest();

            var villa = VillaStore.Villas.FirstOrDefault(x => x.Id == id);


            if (villa == null)
                return NotFound();

            villa.Name = villaDTO.Name;
            villa.Area = villaDTO.Area;
            villa.Occupancy = villaDTO.Occupancy;

            //return Ok(villa);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePatchVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePatchVilla(int id, JsonPatchDocument<VillaDTO> patchVillaDTO)
        {
            // if (!ModelState.IsValid)
            //   return BadRequest(ModelState);


            if (id == 0)
                return BadRequest();

            var villa = VillaStore.Villas.FirstOrDefault(x => x.Id == id);


            if (villa == null)
                return NotFound();

            patchVillaDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //return Ok(villa);
            return NoContent();
        }
    }
}
