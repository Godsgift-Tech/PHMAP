using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PHAPI.Data;
using PHAPI.Models.Domain;
using PHAPI.Models.DTO;

namespace PHAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LGAController : ControllerBase
    {
        private readonly PHDbContext _db;

        public LGAController(PHDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLGA()
        {
            var lgaDomain = await _db.LGAs.ToListAsync();
            //using the lgaDto
            // Decoupling our domain model from the view of the API by using the DTO instead
            var lgaDto = new List<LGADTO>();
            foreach (var lga in lgaDomain)
            {
                lgaDto.Add(new LGADTO()
                {
                    Id = lga.Id,
                    Name = lga.Name,
                    Code = lga.Code,
                    LGAImageUrl = lga.LGAImageUrl,

                });
            }

            return Ok(lgaDto);

        }
        // Get LGA by Id using route
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetLGAById([FromRoute] Guid id)
        {
            var lgaDomain = await _db.LGAs.FindAsync(id);
            if (lgaDomain == null)
            {
                return NotFound();
            }
            var lgaDto = new LGADTO
            {
                Id = lgaDomain.Id,
                Name = lgaDomain.Name,
                Code = lgaDomain.Code,
                LGAImageUrl = lgaDomain.LGAImageUrl,

            };
            // instead of returning domain model we return the DTO
            return Ok(lgaDto);  // this is a 200 rresponse


        }
        //Post for creating a new Lga
        [HttpPost]
        public IActionResult createAnLGA( AddLgaRequetDTO addLgaRequetDTO)
        {
            // convert the DTO to Domain model
            // Use the Domain model to create LGA
            var lgaDomainModel = new LGA
            {
                Code = addLgaRequetDTO.Code,
                Name = addLgaRequetDTO.Name,
                LGAImageUrl = addLgaRequetDTO.LGAImageUrl,

            };
            _db.LGAs.Add(lgaDomainModel);
            _db.SaveChanges();

            return Ok("LGA was added successfully");
           // return Ok(GetAllLGA());
            // return CreatedAtAction(nameof(GetLGAById), new { Id = lgaDomainModel.Id }, lgaDomainModel);
           

        }

        [HttpPut]
        [Route("{id:Guid}")]
        // converting the UpdateDTO to domain model, thereby using it for the update.
        public IActionResult UpdateAnLga(Guid id, UpdateLgaDTO updateLgaDTO)
        {
            var lgaDomain = _db.LGAs.Find(id);
            if (lgaDomain == null)
            {
                return NotFound();
            }

            lgaDomain.Name = updateLgaDTO.Name;
            lgaDomain.Code = updateLgaDTO.Code;
            lgaDomain.LGAImageUrl = updateLgaDTO.LGAImageUrl;
            _db.SaveChanges();
            return Ok(lgaDomain);



        }
        [HttpDelete]
        [Route("{id:Guid}")]
        // deleting the LGA found by id
        public IActionResult Delete(Guid id)
        {
            var lgaDomain = _db.LGAs.Find(id);
            if(lgaDomain == null)
            {
                return NotFound();
            }
            _db.LGAs.Remove(lgaDomain);
            _db.SaveChanges();
            return Ok("Lga was removed successfully");
        } 



    }
}
