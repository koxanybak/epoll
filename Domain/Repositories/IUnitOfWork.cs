using System.Threading.Tasks;

namespace epoll.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}