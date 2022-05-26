using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.SE.Services
{
    public class LoginRepository : ILogin
    {
        private readonly ApplicationDbContext _context;
        DatabaseConnection _conn;
        public LoginRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public bool Delete(long UserId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("UserDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@UserId", SqlDbType.BigInt);
                para.Value = UserId;
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

        public Users Edit(long UserId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UserId", UserId, DbType.Int32);

            Users employee = new Users();

            using (var conn = new SqlConnection(_conn.DbConn))
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    employee = conn.QueryFirst<Users>("UserEdit", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return employee;
        }

        public async Task<LoginRequest> LoginUser(string Email, string Password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Email", Email, DbType.String);
            parameters.Add("Password", Password, DbType.String);
            LoginRequest loginUser = new LoginRequest();

            using (var conn = new SqlConnection(_conn.DbConn))
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    loginUser = await conn.QueryFirstOrDefaultAsync<LoginRequest>("UserLogin", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return loginUser;
        }

        public bool Save(Users tapType)
        {
            using (SqlConnection con = new SqlConnection(_conn.DbConn))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@FirstName", tapType.FirstName);
                param.Add("@LastName", tapType.LastName);
                param.Add("@EmailAddress", tapType.EmailAddress);
                param.Add("@Password", tapType.Password);
                param.Add("@Source", tapType.Source);
                param.Add("@StoreId", tapType.StoreId);
                param.Add("@MiddleName", tapType.MiddleName);
                param.Add("@RoleId", tapType.RoleId);
                param.Add("@HireDate", tapType.HireDate);
                int result = con.Execute("IF NOT EXISTS (SELECT EmailAddress from dbo.[User] where EmailAddress=@EmailAddress)INSERT INTO  dbo.[User] (EmailAddress,Password,Source,FirstName,MiddleName,LastName,RoleId,StoreId,HireDate)VALUES(@EmailAddress,@Password,@Source,@FirstName,@MiddleName,@LastName,@RoleId,@StoreId,@HireDate)", param, null, 0, CommandType.Text);

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Update(Users tapType)
        {
            using (SqlConnection con = new SqlConnection(_conn.DbConn))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@UserId", tapType.UserId);
                param.Add("@FirstName", tapType.FirstName);
                param.Add("@LastName", tapType.LastName);
                param.Add("@EmailAddress", tapType.EmailAddress);
                param.Add("@Password", tapType.Password);
                param.Add("@Source", tapType.Source);
                param.Add("@StoreId", tapType.StoreId);
                param.Add("@MiddleName", tapType.MiddleName);
                param.Add("@RoleId", tapType.RoleId);
                param.Add("@HireDate", tapType.HireDate);
                int result = con.Execute("IF NOT EXISTS (SELECT EmailAddress from dbo.[User] where EmailAddress=@EmailAddress AND UserId<>@UserId) UPDATE dbo.[User] SET FirstName=@FirstName,LastName=@LastName,EmailAddress=@EmailAddress,Password=@Password,Source=@Source,StoreId=@StoreId,MiddleName=@MiddleName,RoleId=@RoleId,HireDate=@HireDate where UserId=@UserId", param, null, 0, CommandType.Text);

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Users ViewEmail(string EmailAddress)
        {
            var parameters = new DynamicParameters();
            parameters.Add("EmailAddress", EmailAddress, DbType.String);

            Users employee = new Users();

            using (var conn = new SqlConnection(_conn.DbConn))
            {

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    employee = conn.QueryFirst<Users>("SELECT RoleId FROM [dbo].[User] where EmailAddress=@EmailAddress", parameters, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return employee;
        }

        public List<Roles> ViewRole()
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var param = new DynamicParameters();
                var ListofPlan = sqlcon.Query<Roles>("SELECT RoleId, RoleDesc FROM Role", param, null, true, 0, commandType: CommandType.Text).ToList();
                return ListofPlan;
            }
        }

        public List<LoginRequest> ViewUser(long StoreId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var param = new DynamicParameters();
                param.Add("@StoreId", StoreId);
                var ListofPlan = sqlcon.Query<LoginRequest>("ViewUser", param, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
    }
}
