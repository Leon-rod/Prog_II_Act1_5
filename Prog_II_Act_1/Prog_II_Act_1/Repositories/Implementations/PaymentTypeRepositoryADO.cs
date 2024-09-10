using Prog_II_Act_1.Entities;
using Prog_II_Act_1.Properties;
using Prog_II_Act_1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Repositories.Implementations
{
    public class PaymentTypeRepositoryADO : IPaymentTypeRepository
    {
        public bool Add(PaymentType paymentType)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_ADD_PAYMENT_TYPE", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", paymentType.ID);
                cmd.Parameters.AddWithValue("@NAME", paymentType.Name);

                result = cmd.ExecuteNonQuery() == 1;
                t.Commit();
            }
            catch (Exception ex)
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

        public List<PaymentType> GetAll()
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            List<PaymentType> list = new List<PaymentType>();
            SqlTransaction? t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_GET_ALL_PAYMENT_TYPES", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                foreach (DataRow row in dt.Rows)
                {
                    PaymentType paymentType = new PaymentType();
                    paymentType.ID = (int)row[0];
                    paymentType.Name = (string)row[1];
                    list.Add(paymentType);
                }
                t.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex}");
                t.Rollback();
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return list;
        }
    }
}
