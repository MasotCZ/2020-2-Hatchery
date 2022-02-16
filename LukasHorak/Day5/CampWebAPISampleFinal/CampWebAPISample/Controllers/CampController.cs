using AutoMapper;
using CampWebAPISample.Data;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampWebAPISample.Controllers;

[Route("api/[controller]")]
public class CampController : Controller
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IMapper _mapper;
    private readonly ICampRepository _repository;

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
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
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
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CampModel>> Post([FromBody] CampModel model)
    {
        try
        {
            var existing = await _repository.GetCampAsync(model.Moniker);
            if (existing != null) return BadRequest("Moniker in Use");

            var location = await _repository.GetLocationAsync(model.LocationAddress, model.LocationPostalCode);
            if (location == null) return BadRequest("Location is not valid");
            // Create a new Camp
            var camp = _mapper.Map<Camp>(model);
            camp.Location = location;

            _repository.Add(camp);
            if (await _repository.SaveChangesAsync())
                return Created($"/api/camps/{camp.Moniker}", _mapper.Map<CampModel>(camp));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        }

        return BadRequest();
    }

    [HttpPut("{moniker}")]
    public async Task<ActionResult<CampModel>> Put(string moniker, [FromBody] CampModel model)
    {
        try
        {
            var oldCamp = await _repository.GetCampAsync(moniker);
            if (oldCamp == null) return NotFound($"Could not find camp with moniker of {moniker}");

            _mapper.Map(model, oldCamp);

            if (await _repository.SaveChangesAsync()) return _mapper.Map<CampModel>(oldCamp);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        }

        return BadRequest();
    }

    [HttpDelete("{moniker}")]
    public async Task<IActionResult> Delete(string moniker)
    {
        try
        {
            var oldCamp = await _repository.GetCampAsync(moniker);
            if (oldCamp == null) return NotFound();

            _repository.Delete(oldCamp);

            if (await _repository.SaveChangesAsync()) return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        }

        return BadRequest("Failed to delete the camp");
    }
}