using BTH.Core.Entities;
using System.Threading.Tasks;

namespace BTH.Core.Services.CoBa.Users
{
    public interface ICoBaUserService
    {
        Task<CoBaUser[]> GetAll();

        Task Update(CoBaUser user);
    }
}
