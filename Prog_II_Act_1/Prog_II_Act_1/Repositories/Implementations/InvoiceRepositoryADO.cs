using Prog_II_Act_1.Entities;
using Prog_II_Act_1.Properties;
using Prog_II_Act_1.Repositories.Contracts;
using Prog_II_Act_1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Repositories.Implementations
{
    public class InvoiceRepositoryADO : IInvoiceRepository
    {
        public bool Delete(int id)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_DELETE_INVOICE_DETAIL", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_INVOICE", id);
                result = cmd.ExecuteNonQuery() > 0;
                if (!result)
                    throw new Exception();
                SqlCommand cmdInvoice = new SqlCommand("SP_DELETE_INVOICE", cnn, t);
                cmdInvoice.CommandType = CommandType.StoredProcedure;
                cmdInvoice.Parameters.AddWithValue("ID_INVOICE", id);
                result = cmdInvoice.ExecuteNonQuery() == 1;
                if (!result) 
                    throw new Exception();
                t.Commit();
            } catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error al intentar eliminar una factura...");
                t.Rollback();
            } finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }

        public List<Invoice> GetAll()
        {
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            List<Invoice> result = new List<Invoice>();
            SqlTransaction? t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_GET_ALL_INVOICES",cnn,t);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                t.Commit();
                foreach(DataRow row in dt.Rows)
                {
                    Invoice inv = new Invoice()
                    {
                        ID = (int)row[0],
                        Date = (DateTime)row[1],
                        PaymenType = new PaymentType()
                        {
                            ID = (int)row[2]
                        },
                        Client = (string) row[3]
                    };
                    result.Add(inv);
                }
            }catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error al tratar de obtener los datos...");
                t.Rollback();
            } finally
            {

                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }

        public Invoice GetById(int id)
        {
            Invoice result = new Invoice();
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                DataTable dt = new DataTable();
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_GET_INVOICE_BY_ID", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_INVOICE", id);
                dt.Load(cmd.ExecuteReader());
                foreach (DataRow row in dt.Rows)
                {
                    result.ID = id;
                    result.Date = (DateTime)row[1];
                    result.PaymenType = new PaymentType()
                    {
                        ID = (int)row[2]
                    };
                    result.Client = (string)row[3];
                }
                t.Commit();
                
            } catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error al obtener una factura por id...");
                t.Rollback();
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }

        public bool Save(Invoice invoice)
        {
            bool result = false;
            SqlConnection cnn = DataHelper.GetInstance().GetConnection();
            SqlTransaction? t = null;
            try
            {
                int IdInvoice = 0;
                int IdInvoiceDetail = 0;
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_ADD_INVOICE", cnn, t);
                SqlParameter sqlParameter = new SqlParameter("@ID_INVOICE", SqlDbType.Int);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DATE", invoice.Date);
                cmd.Parameters.AddWithValue("@ID_PAYMENT_TYPE", invoice.PaymenType.ID);
                cmd.Parameters.AddWithValue("@CLIENT", invoice.Client);
                if (invoice.ID == null)
                {                    
                    sqlParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(sqlParameter);
                }
                else
                {
                    cmd.CommandText = "SP_UPDATE_INVOICE";
                    cmd.Parameters.AddWithValue("@ID_INVOICE",invoice.ID);
                }
                
                result = cmd.ExecuteNonQuery() == 1;
                bool resultHelper = false;
                if (invoice.ID == null)
                {
                    IdInvoice = (int)sqlParameter.Value;
                    IdInvoiceDetail = 1;
                }
                
                foreach(InvoiceDetail inv_det in invoice.InvoiceDetails)
                {
                    SqlCommand cmdDetails = new SqlCommand("SP_ADD_INVOICE_DETAILS",cnn,t);
                    cmdDetails.CommandType = CommandType.StoredProcedure;
                    if (invoice.ID == null)
                    {
                        cmdDetails.Parameters.AddWithValue("@ID_INVOICE", IdInvoice);
                        cmdDetails.Parameters.AddWithValue("@ID_INVOICE_DETAIL", IdInvoiceDetail);
                    }
                    else
                    {
                        cmdDetails.CommandText = "SP_UPDATE_INVOICE_DETAIL";
                        cmdDetails.Parameters.AddWithValue("@ID_INVOICE",invoice.ID);
                        cmdDetails.Parameters.AddWithValue("@ID_INVOICE_DETAIL",inv_det.InvoiceDetailID);
                    }
                    cmdDetails.Parameters.AddWithValue("@ID_ARTICLE", inv_det.DetailArticle.Id);
                    cmdDetails.Parameters.AddWithValue("@AMOUNT", inv_det.Amount);
                    resultHelper = cmdDetails.ExecuteNonQuery() == 1;
                    IdInvoiceDetail++;
                }
                if (resultHelper != result)
                    result = false;
                t.Commit();
            } catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex}");
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
