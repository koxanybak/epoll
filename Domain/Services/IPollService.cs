using System.Collections.Generic;
using System.Threading.Tasks;
using epoll.Domain.Models;
using epoll.Domain.Services.Communication;

namespace epoll.Domain.Services
{
    public interface IPollService
    {
        Task<IEnumerable<Poll>> ToListAsync();
        Task<SavePollResponse> SaveAsync(Poll poll);
        Task<Poll> FindAsync(long id);
        Task<SavePollResponse> VoteAsync(long pollId, long optionId);
    }
}