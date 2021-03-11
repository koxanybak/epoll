using System.Collections.Generic;
using System.Threading.Tasks;
using epoll.Domain.Models;

namespace epoll.Domain.Repositories
{
    public interface IPollRepository
    {
        Task<IEnumerable<Poll>> ToListAsync();
        Task AddAsync(Poll poll);
        Task<Poll> FindAsync(long id);
        void Update(PollOption pollOption);
    }
}