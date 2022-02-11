using AutoMapper;
using CampWebAPISample.Data;
using CampWebAPISample.Models;
using Microsoft.AspNetCore.Mvc;
using CampWebAPISample.Data.Entities;

namespace CampWebAPISample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampController : Controller
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CampController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<CampModel[]>> Get(bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);

                return _mapper.Map<CampModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);

                if (result == null) return NotFound();

                return _mapper.Map<CampModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        public async Task<ActionResult<CampModel>> Post([FromBody] CampModel model)
        {
            try
            {
                var existing = await _repository.GetCampAsync(model.Moniker);
                if (existing != null)
                {
                    return BadRequest("Moniker in Use");
                }

                var location = await _repository.GetLocationAsync(model.LocationAddress, model.LocationPostalCode);
                if (location == null)
                {
                    return BadRequest("Location is not valid");
                }
                // Create a new Camp
                var camp = _mapper.Map<Camp>(model);
                camp.Location = location;

                _repository.Add(camp);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/camps/{camp.Moniker}", _mapper.Map<CampModel>(camp));
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModel>> Put(string moniker, [FromBody] CampModel model)
        {
            try
            {
                var currentCamp = await _repository.GetCampAsync(moniker);

                if (currentCamp is null)
                {
                    return NotFound("Does not exist");
                }

                //tohle namapuje ten vysledek
                //bez nej bys to musel udelat pres add a remove
                //a nezapomenou pridat do toho modelu incoming veci co nevi jako treba ID z originalu

                _mapper.Map(model, currentCamp);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CampModel>(currentCamp);
                }

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)
        {
            try
            {
                var currentCamp = await _repository.GetCampAsync(moniker);

                if (currentCamp is null)
                {
                    return NotFound();
                }

                _repository.Delete(currentCamp);
                await _repository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

    }

}


