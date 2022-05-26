using DCMS.SE.Data;
using DCMS.SE.Data.Apimodel;
using DCMS.SE.Data.Setting;
using System.Collections.Generic;

namespace DCMS.SE.Services.Interface
{
    public interface IRole
    {
        List<Role> GetAll();
        List<Privilege> GetAllPrivilege();
        List<Privilege> GetAllPrivilege(int  roleId);
        bool DeleteRolePriviliage(int StoreId, int RoleId);
        Privilege PriviliageCheck(string FormName, int RoleId, int StoreId);
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
