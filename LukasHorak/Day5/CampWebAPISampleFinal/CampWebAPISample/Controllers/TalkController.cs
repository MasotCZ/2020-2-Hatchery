using AutoMapper;
using CampWebAPISample.Data;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampWebAPISample.Controllers;

[Route("api/camp/{moniker}/[controller]")]
[ApiController]
public class TalkController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICampRepository _repository;

    public TalkController(ICampRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<TalkModel[]>> Get(string moniker)
    {
        try
        {
            var talks = await _repository.GetTalksByMonikerAsync(moniker, true);
            return _mapper.Map<TalkModel[]>(talks);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TalkModel>> Get(string moniker, int id)
    {
        try
        {
            var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);
            if (talk == null) return NotFound($"Could not find a tak with id of {id}");
            return _mapper.Map<TalkModel>(talk);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel talk)
    {
        try
        {
            var camp = await _repository.GetCampAsync(moniker);
            if (camp == null) return BadRequest("Camp does not exist");

            var talkRepository = _mapper.Map<Talk>(talk);
            talkRepository.Camp = camp;

            if (talk.Speaker == null) return BadRequest("Speaker ID is required");
            var speaker = await _repository.GetSpeakerAsync(talk.Speaker.SpeakerId);
            if (speaker == null) return BadRequest("Speaker could not be found");
            talkRepository.Speaker = speaker;

            _repository.Add(talkRepository);

            if (await _repository.SaveChangesAsync())
                return Created($"/api/camps/{moniker}/talk/{talkRepository.TalkId}",
                    _mapper.Map<TalkModel>(talkRepository));
            return BadRequest("Failed to save new Talk");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create Talks");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model)
    {
        try
        {
            model.TalkId = id;
            var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);
            if (talk == null) return NotFound("Couldn't find the talk");

            _mapper.Map(model, talk);


            if (model.Speaker != null)
            {
                var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);
                if (speaker != null) talk.Speaker = speaker;
            }

            //talk.Camp = camp;
            if (await _repository.SaveChangesAsync())
                return _mapper.Map<TalkModel>(talk);
            return BadRequest("Failed to update database.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update Talks");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(string moniker, int id)
    {
        try
        {
            var talk = await _repository.GetTalkByMonikerAsync(moniker, id);
            if (talk == null) return NotFound("Failed to find the talk to delete");
            _repository.Delete(talk);
            if (await _repository.SaveChangesAsync())
                return Ok();
            return BadRequest("Failed to update database.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}