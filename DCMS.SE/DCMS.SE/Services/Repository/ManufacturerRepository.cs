using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.ViewModel;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class ManufacturerRepository : IManufacturer
    {
       private readonly ApplicationDbContext _context;
       private readonly DatabaseConnection _conn;
        public ManufacturerRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public string SerialNoCode(int StoreId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@StoreId", StoreId);
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(ManufacturerCode+1),1) FROM Manufacturer where StoreId=@StoreId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }
        public bool Delete(int ManufacturerId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("ManufacturerDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@ManufacturerId", SqlDbType.Int);
                para.Value = ManufacturerId;
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

        public Manufacturer Edit(int id)
        {
            Manufacturer returnView = _context.Manufacturer.Find(id);
            return returnView;
        }

        public List<ManufacturerView> GetAll(int StoreId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@StoreId", StoreId);
                var ListofPlan = sqlcon.Query<ManufacturerView>("SELECT dbo.Manufacturer.ManufacturerId,dbo.Manufacturer.Email, dbo.Manufacturer.ManufacturerName, dbo.Manufacturer.ManufacturerCode, dbo.AccountGroup.AccountGroupId, dbo.AccountGroup.AccountGroupName, dbo.Area.AreaName, dbo.Manufacturer.IsDefault,  dbo.Manufacturer.Phone, dbo.Manufacturer.Mobile, dbo.Area.AreaId FROM            dbo.Manufacturer INNER JOIN dbo.AccountGroup ON dbo.Manufacturer.AccountGroupId = dbo.AccountGroup.AccountGroupId INNER JOIN  dbo.Area ON dbo.Manufacturer.AreaId = dbo.Area.AreaId where Manufacturer.StoreId=@StoreId", para, null, true, 0, commandType: CommandType.Text).ToList();
                return ListofPlan;
            }
        }
        public List<ManufacturerView> ViewAllCustomer(int StoreId)
        {
            var result = (from a in _context.Manufacturer
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.StoreId == StoreId && a.AccountGroupId == 26
                          select new ManufacturerView
                          {
                              ManufacturerId = a.ManufacturerId,
                              ManufacturerCode = a.ManufacturerCode,
                              ManufacturerName = a.ManufacturerName,
                              Phone = a.Phone,
                              Email = a.Email,
                              Country = a.Country,
                              City = a.City,
                              AccountGroupName = b.AccountGroupName
                          }).ToList();
            return result;
        }
        public List<ManufacturerView> ViewAllSupplier(int StoreId)
        {
            var result = (from a in _context.Manufacturer
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.StoreId == StoreId && a.AccountGroupId == 22
                          select new ManufacturerView
                          {
                              ManufacturerId = a.ManufacturerId,
                              ManufacturerCode = a.ManufacturerCode,
                              ManufacturerName = a.ManufacturerName,
                              Phone = a.Phone,
                              Email = a.Email,
                              Country = a.Country,
                              City = a.City,
                              AccountGroupName = b.AccountGroupName
                          }).ToList();
            return result;
        }
        public int Save(Manufacturer model)
        {
            _context.Manufacturer.Add(model);
            _context.SaveChanges();
            int id = model.ManufacturerId;
            return id;
        }

        public void Update(Manufacturer model)
        {
            _context.Manufacturer.Update(model);
            _context.SaveChanges();
        }
        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.Manufacturer
                               where progm.ManufacturerCode == name
                               select progm.ManufacturerId).Count();
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
            var checkResult = (from progm in _context.Manufacturer
                               where progm.ManufacturerCode == name
                               select progm.ManufacturerId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Manufacturer
                                    where progm.ManufacturerCode == name
                                    select progm.ManufacturerId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }
        public List<Manufacturer> GetCashFill()
        {
            var result = (from a in _context.Manufacturer
                          select new Manufacturer
                          {
                              ManufacturerId = a.ManufacturerId,
                              ManufacturerCode = a.ManufacturerCode
                          }).ToList();
            return result;
        }
        public List<Manufacturer> GetBankFill()
        {
            var result = (from a in _context.Manufacturer
                          select new Manufacturer
                          {
                              ManufacturerId = a.ManufacturerId,
                              ManufacturerCode = a.ManufacturerCode
                          }).ToList();
            return result;
        }

        public List<ManufacturerView> ViewAllExpensesCategory(int id)
        {
            var result = (from a in _context.Manufacturer
                          join b in _context.AccountGroup on a.AccountGroupId equals b.AccountGroupId
                          where a.StoreId == id && a.AccountGroupId == 13 || a.AccountGroupId == 15
                          select new ManufacturerView
                          {
                              ManufacturerId = a.ManufacturerId,
                              ManufacturerCode = a.ManufacturerCode,
                              ManufacturerName = a.ManufacturerName,
                              Phone = a.Phone,
                              Email = a.Email,
                              Country = a.Country,
                              City = a.City,
                              AccountGroupName = b.AccountGroupName
                          }).ToList();
            return result;
        }

        public List<AccountGroup> ExpensesCategory()
        {
            var result = (from a in _context.AccountGroup
                          where a.AccountGroupId == 13 || a.AccountGroupId == 15
                          select new AccountGroup
                          {
                              AccountGroupId = a.AccountGroupId,
                              AccountGroupName = a.AccountGroupName
                          }).ToList();
            return result;
        }
    }
}
