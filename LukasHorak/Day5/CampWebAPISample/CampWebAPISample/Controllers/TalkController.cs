using AutoMapper;
using CampWebAPISample.Data;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampWebAPISample.Controllers
{
    [Route("api/camp/{moniker}/[controller]")]
    [ApiController]
    public class TalkController : ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public TalkController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TalkModel[]>> Get(string moniker, bool includeTalks)
        {
            try
            {
                var talks = await _repository.GetTalksByMonikerAsync(moniker, includeTalks);

                if (talks is null)
                {
                    return NotFound();
                }

                return _mapper.Map<TalkModel[]>(talks);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TalkModel>> Get(string moniker, int id, bool includeTalks)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, includeTalks);

                if (talk is null)
                {
                    return NotFound();
                }

                return _mapper.Map<TalkModel>(talk);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel model)
        {
            try
            {
                var talk = _mapper.Map<Talk>(model);
                var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);

                talk.Speaker = speaker;
                talk.Camp = await _repository.GetCampAsync(moniker);

                if (talk.Camp is null)
                {
                    return BadRequest();
                }

                _repository.Add(talk);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/camps/{talk.Camp.Moniker}/talk/{talk.TalkId}", _mapper.Map<TalkModel>(talk));
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id);
                var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);

                if (speaker is null)
                {
                    return NotFound();
                }

                if (talk is null)
                {
                    return NotFound();
                }

                _mapper.Map(model, talk);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<TalkModel>(talk);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        public async Task<ActionResult<TalkModel>> Delete(string moniker, int id)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id);

                if (talk is null)
                {
                    return NotFound();
                }

                _repository.Delete(talk);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
