using Prog_II_Act_1.Entities;
using Prog_II_Act_1.Properties;
using Prog_II_Act_1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Repositories.Implementations
{
    internal class InvoiceDetailRepositoryADO : IInvoiceDetailRepository
    {
        public List<InvoiceDetail> GetAll()
        {
            List<InvoiceDetail> result = new List<InvoiceDetail>();
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_GET_ALL_INVOICE_DETAILS", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                foreach (DataRow row in dt.Rows)
                {
                    InvoiceDetail inv_det = new InvoiceDetail()
                    {
                        InvoiceDetailID = (int)row[0],
                        InvoiceID = (int)row[1],
                        DetailArticle = new Article()
                        {
                            Id = (int)row[2]
                        },
                        Amount = (int)row[3]
                    };
                    result.Add(inv_det);
                }
                t.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                t.Rollback();
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }

        public List<InvoiceDetail> GetById(int id)
        {
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            List<InvoiceDetail> result = new List<InvoiceDetail>();
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_GET_DETAIL_BY_ID",cnn,t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_INVOICE",id);
                dt.Load(cmd.ExecuteReader());
                foreach (DataRow row in dt.Rows)
                {
                    InvoiceDetail inv_det = new InvoiceDetail()
                    {
                        InvoiceDetailID =(int) row[0],
                        InvoiceID = (int) row[1],
                        DetailArticle = new Article()
                        {
                            Id = (int) row[2],
                        },
                        Amount = (int) row[3]
                    };
                    result.Add(inv_det);
                }
                t.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error al intentar traer un detalle de factura por Id...");
                t.Rollback();
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }
    }
}
