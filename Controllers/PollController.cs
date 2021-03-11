using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using epoll.Domain.Models;
using epoll.Domain.Services;
using AutoMapper;
using epoll.Resources;
using epoll.Extensions;

namespace epoll.Controllers
{
    [Route("/api/poll")]
    [ApiController]
    public class PollController : Controller
    {
        private readonly IPollService _pollService;
        private readonly IMapper _mapper;

        public PollController(IPollService pollService, IMapper mapper)
        {
            _pollService = pollService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllPollResource>> GetAllAsync()
        {
            var polls = await _pollService.ToListAsync();

            // Remove 'options' from the model
            var pollResources = _mapper.Map<IEnumerable<Poll>, IEnumerable<GetAllPollResource>>(polls);

            return pollResources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSinglePollResource>> GetSingleAsync(long id)
        {
            var poll = await _pollService.FindAsync(id);

            // Not found
            if (poll == null) return NotFound();

            var pollResource = _mapper.Map<Poll, GetSinglePollResource>(poll);
            return pollResource;
        }

        [HttpPost]
        public async Task<ActionResult<GetSinglePollResource>> PostAsync([FromBody] SavePollResource resource)
        {
            // Malformatted data etc.
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var poll = _mapper.Map<SavePollResource, Poll>(resource);
            var result = await _pollService.SaveAsync(poll);

            // Error with DB.
            // (According to one article 400 is an appropriate response in this case.
            // I personally think that responding with 500 is better in this case but went with 400
            // in case it's some kind of best practice not to explicitly respond with 500 with ASP.NET.)
            if (!result.Success) return BadRequest(result.Message);

            var pollResource = _mapper.Map<Poll, GetSinglePollResource>(result.Poll);

            return Ok(pollResource);
        }

        [HttpPost("{pollId}/vote/{optionId}")]
        public async Task<ActionResult<GetSinglePollResource>> VoteAsync(long pollId, long optionId)
        {
            var result = await _pollService.VoteAsync(pollId, optionId);

            // Error with DB.
            if (!result.Success) return BadRequest(result.Message);

            var pollResource = _mapper.Map<Poll, GetSinglePollResource>(result.Poll);

            return Ok(pollResource);
        }
    }
}
