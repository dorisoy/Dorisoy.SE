using DCMS.SE.Data;
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
    public class VoucherTypeRepository : IVoucherType
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public VoucherTypeRepository(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public VoucherType Edit(int id)
        {
            VoucherType returnView = _context.VoucherType.Find(id);
            return returnView;
        }
        public List<VoucherType> GetAll()
        {
            var view = _context.VoucherType.ToList();
            return view;
        }

        public void Update(VoucherType voucherType)
        {
            SqlConnection sqlcon = new (_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new ("UPDATE VoucherType SET VoucherTypeName=@VoucherTypeName,StartIndex=@StartIndex,Prefix=@Prefix,Suffix=@Suffix,StoreId=@StoreId,ShowNote=@ShowNote,ShowAddress=@ShowAddress,ShowEmail=@ShowEmail,ShowPhone=@ShowPhone,ShowDiscount=@ShowDiscount,ShowTax=@ShowTax,ShowBarcode=@ShowBarcode,IsActive=@IsActive where VoucherTypeId=@VoucherTypeId", sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@VoucherTypeId", SqlDbType.Int);
                para.Value = voucherType.VoucherTypeId;
                para = cmd.Parameters.Add("@VoucherTypeName", SqlDbType.NVarChar);
                para.Value = voucherType.VoucherTypeName;
                para = cmd.Parameters.Add("@StartIndex", SqlDbType.NVarChar);
                para.Value = voucherType.StartIndex;
                para = cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar);
                para.Value = voucherType.Prefix;
                para = cmd.Parameters.Add("@Suffix", SqlDbType.NVarChar);
                para.Value = voucherType.Suffix;
                para = cmd.Parameters.Add("@StoreId", SqlDbType.Int);
                para.Value = voucherType.StoreId;
                para = cmd.Parameters.Add("@ShowNote", SqlDbType.NVarChar);
                para.Value = voucherType.ShowNote;
                para = cmd.Parameters.Add("@ShowAddress", SqlDbType.NVarChar);
                para.Value = voucherType.ShowAddress;
                para = cmd.Parameters.Add("@ShowEmail", SqlDbType.NVarChar);
                para.Value = voucherType.ShowEmail;
                para = cmd.Parameters.Add("@ShowPhone", SqlDbType.NVarChar);
                para.Value = voucherType.ShowPhone;
                para = cmd.Parameters.Add("@ShowDiscount", SqlDbType.NVarChar);
                para.Value = voucherType.ShowDiscount;
                para = cmd.Parameters.Add("@ShowTax", SqlDbType.NVarChar);
                para.Value = voucherType.ShowTax;
                para = cmd.Parameters.Add("@ShowBarcode", SqlDbType.NVarChar);
                para.Value = voucherType.ShowBarcode;
                para = cmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                para.Value = voucherType.IsActive;
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
    }
}
