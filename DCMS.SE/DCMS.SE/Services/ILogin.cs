using DCMS.SE.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCMS.SE.Services
{
    public interface ILogin
    {
        Task<LoginRequest> LoginUser(string userName, string passWord);
        bool Save(Users request);
        bool Update(Users request);
        bool Delete(long Id);
        Users Edit(long Id);
        Users ViewEmail(string email);
        List<LoginRequest> ViewUser(long Id);
        List<Roles> ViewRole();
    }
}
