using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
     //   private readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        //public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDBContext db)
        //{
        //    _logger = logger;
        //    _db = db;
        //}
        public VillaAPIController(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        //private readonly ILogging _logger;
        //public VillaAPIController(ILogging logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            var villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
        }

        [HttpGet("{Id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVilla(int Id)
        {
            if (Id == 0)
                return BadRequest();

            var villa=  await _db.Villas.FirstOrDefaultAsync(x => x.Id == Id);

            if (villa == null)
                return NotFound();

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> SaveVilla([FromBody]VillaCreateDTO villa)
        {
            if (_db.Villas.Any(x => x.Name == villa.Name))
            {
                ModelState.AddModelError("CustomerError", "Villa Name already exists");
                return BadRequest(ModelState);
            }

            if (villa == null)
                return BadRequest(villa);

            if (villa.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
            var villObj = _mapper.Map<Villa>(villa);
            //var villObj = new Villa
            //{
            //    Id = villa.Id,
            //    Name = villa.Name,
            //    Details = villa.Details,
            //    ImageURl = villa.ImageURl,
            //    Amenity = villa.Amenity,
            //    Occupancy = villa.Occupancy,
            //    Area = villa.Area,
            //    CreatedDate = DateTime.Now
            //};

            await _db.Villas.AddAsync(villObj);
            await _db.SaveChangesAsync();
            //return Ok(villa);
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            // if (!ModelState.IsValid)
            //   return BadRequest(ModelState);

            
            if (id == 0)
                return BadRequest();

            var villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);


            if (villa == null)
                return NotFound();

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            //return Ok(villa);
            return NoContent();
        }
        [HttpPut("{id:int}", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDTO)
        {
            // if (!ModelState.IsValid)
            //   return BadRequest(ModelState);


            if (id == 0)
                return BadRequest();

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


            if (villa == null)
                return NotFound();
            var villObj = _mapper.Map<Villa>(villaDTO);

            //Villa villObj = new ()
            //{
            //    Id = villaDTO.Id,
            //    Name = villaDTO.Name,
            //    Details = villaDTO.Details,
            //    ImageURl = villaDTO.ImageURl,
            //    Amenity = villaDTO.Amenity,
            //    Occupancy = villaDTO.Occupancy,
            //    Area = villaDTO.Area,
            //    CreatedDate = DateTime.Now
            //};

            _db.Villas.Update(villObj);
            await _db.SaveChangesAsync();

            //return Ok(villa);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePatchVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePatchVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchVillaDTO)
        {
            // if (!ModelState.IsValid)
            //   return BadRequest(ModelState);


            if (id == 0)
                return BadRequest();

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


            if (villa == null)
                return NotFound();

            var villDTOObj = _mapper.Map<VillaUpdateDTO>(villa);

            //var villDTOObj = new  
            //{
            //    Id = villa.Id,
            //    Name = villa.Name,
            //    Details = villa.Details,
            //    ImageURl = villa.ImageURl,
            //    Amenity = villa.Amenity,
            //    Occupancy = villa.Occupancy,
            //    Area = villa.Area
            //};

            patchVillaDTO.ApplyTo(villDTOObj, ModelState);

            var villObj = _mapper.Map<Villa>(villDTOObj);

            //var villObj = new Villa
            //{
            //    Id = villDTOObj.Id,
            //    Name = villDTOObj.Name,
            //    Details = villDTOObj.Details,
            //    ImageURl = villDTOObj.ImageURl,
            //    Amenity = villDTOObj.Amenity,
            //    Occupancy = villDTOObj.Occupancy,
            //    Area = villDTOObj.Area
            //};

            _db.Villas.Update(villObj);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //return Ok(villa);
            return NoContent();
        }
    }
}
