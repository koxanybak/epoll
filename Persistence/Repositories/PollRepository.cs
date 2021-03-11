using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using epoll.Domain.Models;
using epoll.Domain.Repositories;
using epoll.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace epoll.Persistence.Repositories
{
    public class PollRepository : BaseRepository, IPollRepository
    {
        public PollRepository(AppDbContext context) : base(context)
        {}

        public async Task<IEnumerable<Poll>> ToListAsync()
        {
            return await _context.Polls.ToListAsync();
        }

        public async Task<Poll> FindAsync(long id)
        {
            return await _context.Polls.Include(p => p.Options).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Poll poll)
        {
            await _context.Polls.AddAsync(poll);
        }

        public void Update(PollOption pollOption)
        {
            _context.PollOptions.Update(pollOption);
        }
    }
}