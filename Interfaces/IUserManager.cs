using System.Collections.Generic;
using System.Threading.Tasks;
using BetterGeekApi.Model;

namespace BetterGeekApi.Interfaces
{
    public interface IUserManager : IEntityManager<User>
    {
        Task syncUser(string id);
    }
}