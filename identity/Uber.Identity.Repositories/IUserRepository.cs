using Uber.Common.Repositories;
using Uber.Identity.Domain.Entities;

namespace Uber.Identity.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
