using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Apimodel;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Setting;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class RolesRepository : IRole
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public RolesRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.Role
                                     where progm.RoleDesc == name
                               select progm.RoleId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckRoleName(int RoleId)
        {
            var checkResult = (from progm in _context.Privilege
                               where progm.RoleId == RoleId
                               select progm.RoleId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int CheckNameId(string name)
        {
            var checkResult = (from progm in _context.Role
                               where progm.RoleDesc == name
                               select progm.RoleId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Role
                                    where progm.RoleDesc == name
                                    select progm.RoleId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int RoleId)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new ("IF NOT EXISTS (SELECT RoleId from User where RoleId=@RoleId) DELETE FROM Role where RoleId=@RoleId", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@RoleId", SqlDbType.Int);
                para.Value = RoleId;
                long rowAffacted = cmd.ExecuteNonQuery();
                if (rowAffacted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public Role Edit(int id)
        {
            Role returnView = _context.Role.Find(id);
            return returnView;
        }

        public List<Role> GetAll()
        {
            var result = (from progm in _context.Role
                          where progm.RoleId != 1
                          select new Role
                          {
                              RoleId = progm.RoleId,
                              RoleDesc = progm.RoleDesc
                          }).ToList();
            return result;
        }
        public List<Privilege> GetAllPrivilege()
        {
            var view = _context.Privilege.ToList();
            return view;
        }
        public List<Privilege> GetAllPrivilege(int roleid)
        {
            var result = (from progm in _context.Privilege
                               where progm.RoleId == roleid
                               select new Privilege
                               {
                                   PrivilegeId = progm.PrivilegeId,
                                   FormName = progm.FormName,
                                   AddAction = progm.AddAction,
                                   EditAction = progm.EditAction,
                                   DeleteAction = progm.DeleteAction,
                                   ShowAction = progm.ShowAction
                               }).ToList();
            return result;
        }
        public int Save(Role model)
        {
            _context.Role.Add(model);
            _context.SaveChanges();
            int id = model.RoleId;
            return id;
        }

        public void Update(Role model)
        {
            _context.Role.Update(model);
            _context.SaveChanges();
        }
        public bool DeleteRolePriviliage(int StoreId, int RoleId)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new ("DELETE FROM Privilege where StoreId=@StoreId AND RoleId=@RoleId", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@StoreId", SqlDbType.Int);
                para.Value = StoreId;
                para = cmd.Parameters.Add("@RoleId", SqlDbType.Int);
                para.Value = RoleId;
                int rowAffacted = cmd.ExecuteNonQuery();
                if (rowAffacted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public void SavePrivilige(Privilege tapType)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new ("INSERT INTO Privilege (FormName,FormNameNepali,SettingType,AddAction,EditAction,DeleteAction,ShowAction,RoleId,StoreId,AddedDate,IsActive)values(@FormName,@FormNameNepali,@SettingType,@AddAction,@EditAction,@DeleteAction,@ShowAction,@RoleId,@StoreId,@AddedDate,@IsActive)", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@FormName", SqlDbType.NVarChar);
                para.Value = tapType.FormName;
                para = cmd.Parameters.Add("@FormNameNepali", SqlDbType.NVarChar);
                para.Value = tapType.FormNameNepali;
                para = cmd.Parameters.Add("@SettingType", SqlDbType.NVarChar);
                para.Value = tapType.SettingType;
                para = cmd.Parameters.Add("@AddAction", SqlDbType.Bit);
                para.Value = tapType.AddAction;
                para = cmd.Parameters.Add("@EditAction", SqlDbType.Bit);
                para.Value = tapType.EditAction;
                para = cmd.Parameters.Add("@DeleteAction", SqlDbType.Bit);
                para.Value = tapType.DeleteAction;
                para = cmd.Parameters.Add("@ShowAction", SqlDbType.Bit);
                para.Value = tapType.ShowAction;
                para = cmd.Parameters.Add("@RoleId", SqlDbType.Int);
                para.Value = tapType.RoleId;
                para = cmd.Parameters.Add("@StoreId", SqlDbType.Int);
                para.Value = tapType.StoreId;
                para = cmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                para.Value = tapType.IsActive;
                para = cmd.Parameters.Add("@AddedDate", SqlDbType.DateTime);
                para.Value = tapType.AddedDate;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public Privilege PriviliageCheck(string FormName, int RoleId, int StoreId)
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@FormName", FormName);
                para.Add("@RoleId", RoleId);
                para.Add("@StoreId", StoreId);
                var result = sqlcon.Query<Privilege>("PrivilegeCheck", para, null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }
    }
}
