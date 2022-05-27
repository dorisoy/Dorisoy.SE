using Dapper;
using DCMS.SE.Data;
using DCMS.SE.Data.Connection;
using DCMS.SE.Data.Inventory;
using DCMS.SE.Data.Setting;
using DCMS.SE.Data.ViewModel;
using DCMS.SE.Services.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DCMS.SE.Services.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public ProductRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }

        public bool CheckName(string name)
        {
            var checkResult = (from progm in _context.Product
                               where progm.ProductName == name
                               select progm.ProductId).Count();
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
            var checkResult = (from progm in _context.Product
                               where progm.ProductName == name
                               select progm.ProductId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Product
                                    where progm.ProductName == name
                                    select progm.ProductId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int ProductId)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            //SqlTransaction transaction;

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                    //transaction = sqlcon.BeginTransaction();
                }
                //transaction = sqlcon.BeginTransaction();
                SqlCommand cmd = new ("ProductDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@ProductId", SqlDbType.Int);
                para.Value = ProductId;
                long rowAffacted = cmd.ExecuteNonQuery();
                //transaction.Commit();
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

        public Product Edit(int id)
        {
            Product returnView = _context.Product.Find(id);
            return returnView;
        }
        public List<ProductView> ViewAllProduct(int StoreId)
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@StoreId", StoreId);
                var ListofPlan = sqlcon.Query<ProductView>("ViewAllProduct", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
        public List<ProductView> ViewCategoryWiseProduct(int catagoryId)
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@catagoryId", catagoryId);
                var ListofPlan = sqlcon.Query<ProductView>("ViewAllCategory", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
            //var result = (from a in _context.Product
            //                   join b in _context.Brand on a.BrandId equals b.BrandId
            //                   join d in _context.Unit on a.UnitId equals d.UnitId
            //                   join e in _context.Catagory on a.CatagoryId equals e.CatagoryId
            //                where a.StoreId == StoreId
            //              select new ProductView
            //              {
            //                       ProductId = a.ProductId,
            //                       ProductCode = a.ProductCode,
            //                       ProductName = a.ProductName,
            //                       SalesRate = a.SalesRate,
            //                       Image = a.Image,
            //                       IsActive = a.IsActive,
            //                       CatagoryName = e.CatagoryName,
            //                       UnitName = d.UnitName,
            //                       BrandName = b.Name
            //                   }).ToList();
            //return result;
        }



        public string GetProductNo(int StoreId)
        {
            using (SqlConnection sqlcon = new (_conn.DbConn))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@StoreId", StoreId);
                return val = sqlcon.Query<string>("SELECT ISNULL( MAX(productCode+1),1) FROM Product where StoreId=@StoreId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
            }
        }

        public int Save(Product model)
        {
            _context.Product.Add(model);
            _context.SaveChanges();
            int id = model.ProductId;
            return id;
        }

        public void Update(Product model)
        {
            _context.Product.Update(model);
            _context.SaveChanges();
        }
    }
}
