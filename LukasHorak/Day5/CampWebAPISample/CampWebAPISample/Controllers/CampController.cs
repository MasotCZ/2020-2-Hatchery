using AutoMapper;
using CampWebAPISample.Data;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampWebAPISample.Controllers
{
    [Route("api/[controller]")]
    public class CampController : Controller
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public CampController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCampsAsync()
        {
            try
            {
                var res = _mapper.Map<CampModel[]>(await _repository.GetAllCampsAsync());
                return Ok(res);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db fail");
            }
        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> GetCampAsync(string moniker)
        {
            try
            {
                var res = _mapper.Map<CampModel>(await _repository.GetCampAsync(moniker));

                if (res is null)
                {
                    return NotFound("No Data");
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db fail");
            }
        }
        /*
        [HttpGet("{includeTalks}")]
        public async Task<ActionResult<CampModel[]>> Get([FromBody] bool includeTalks)
        {
            try
            {
                var res = _mapper.Map<CampModel[]>(await _repository.GetAllCampsAsync(includeTalks));
                return BadRequest();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db fail");
            }
        }
        */

        public async Task<ActionResult<CampModel>> Post([FromBody] CampModel model)
        {
            try
            {
                var existing = await _repository.GetCampAsync(model.Moniker);

                if (existing is not null)
                {
                    return BadRequest("Moniker already used, must be unique");
                }

                var camp = _mapper.Map<CampModel, Camp>(model);
                _repository.Add(camp);
                await _repository.SaveChangesAsync();
                return Ok(camp);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db fail");
            }
        }
    }
}
