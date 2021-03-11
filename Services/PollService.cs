using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using epoll.Domain.Models;
using epoll.Domain.Repositories;
using epoll.Domain.Services;
using epoll.Domain.Services.Communication;

namespace epoll.Services
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;
        private readonly IUnitOfWork _unitOfWork; // Saves changes made to the context to the database.

        public PollService(IPollRepository pollRepository, IUnitOfWork unitOfWork)
        {
            _pollRepository = pollRepository;
            _unitOfWork = unitOfWork;
        }

        // GET /
        public async Task<IEnumerable<Poll>> ToListAsync()
        {
            return await _pollRepository.ToListAsync();
        }

        // GET /{id}
        public async Task<Poll> FindAsync(long id)
        {
            return await _pollRepository.FindAsync(id);
        }

        // POST /
        public async Task<SavePollResponse> SaveAsync(Poll poll)
        {
            try
            {
                await _pollRepository.AddAsync(poll);
                await _unitOfWork.CompleteAsync();

                return new SavePollResponse(poll);
            }
            catch (Exception ex)
            {
                return new SavePollResponse($"Could not save poll: {ex.Message}");
            }
        }

        // POST /{pollId}/vote/{optionId}
        public async Task<SavePollResponse> VoteAsync(long pollId, long optionId)
        {
            // Fetching the poll is unnecessary since the whole operation could be
            // completed by fetching only the 'poll_option' but I think that the client should
            // be sent 400 if the poll doesn't exists or have that specific option id.

            var pollInDb = await _pollRepository.FindAsync(pollId);
            if (pollInDb == null) {
                return new SavePollResponse("Poll not found");
            }

            var optInDb = pollInDb.Options.Find(p => p.Id == optionId);
            if (optInDb == null) {
                return new SavePollResponse("Poll option not found");
            }

            optInDb.Votes += 1;

            try
            {
                _pollRepository.Update(optInDb);
                await _unitOfWork.CompleteAsync();

                return new SavePollResponse(pollInDb);
            }
            catch (Exception ex)
            {
                return new SavePollResponse($"Could not update poll: {ex.Message}");
            }
        }
    }
}