using AutoMapper;
using CampWebAPISample.Data;
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

                if (res is null) {
                    return NotFound("No Data");
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "db fail");
            }
        }
    }
}
