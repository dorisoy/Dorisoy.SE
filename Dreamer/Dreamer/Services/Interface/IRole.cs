using Dreamer.Data;
using Dreamer.Data.Apimodel;
using Dreamer.Data.Setting;
using System.Collections.Generic;

namespace Dreamer.Services.Interface
{
    public interface IRole
    {
        List<Role> GetAll();
        List<Privilege> GetAllPrivilege();
        List<Privilege> GetAllPrivilege(int  roleId);
        bool DeleteRolePriviliage(int CompanyId, int RoleId);
        Privilege PriviliageCheck(string FormName, int RoleId, int CompanyId);
        void SavePrivilige(Privilege privilege);
        bool CheckName(string name);
        bool CheckRoleName(int RoleId);
        int CheckNameId(string name);
        Role Edit(int id);
        int Save(Role model);
        void Update(Role model);
        bool Delete(int id);
    }
}
