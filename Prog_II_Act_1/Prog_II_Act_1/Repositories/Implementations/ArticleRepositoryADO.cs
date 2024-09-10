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
    public class ArticleRepositoryADO : IArticleRepository
    {
        public bool Add(Article article)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_ADD_ARTICLE",cnn,t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NAME", article.Name);
                cmd.Parameters.AddWithValue("@UNIT_PRICE", article.Unit_Price);

                result = cmd.ExecuteNonQuery() == 1;
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

        public bool Delete(Article article)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection( Resources.CnnString);
            SqlTransaction? t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_DELETE_ARTICLE",cnn,t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_ARTICLE",article.Id);
                result = cmd.ExecuteNonQuery() == 1;
                t.Commit();
            } catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex}");
                t.Rollback();
            } finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }

        public List<Article> GetAll()
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            List<Article> list = new List<Article>();
            SqlTransaction? t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_GET_ALL_ARTICLES",cnn,t);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                foreach(DataRow row in dt.Rows)
                {
                    Article article = new Article();
                    article.Id = (int)row[0];
                    article.Name = (string)row[1];
                    article.Unit_Price = (decimal)row[2];
                    list.Add(article);
                }
                t.Commit();

            } catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex}");
                t.Rollback();
            } finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return list;
        }

        public bool Save(Article article)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction? t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_UPDATE_ARTICLE", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_ARTICLE", article.Id);
                cmd.Parameters.AddWithValue("@NAME",article.Name);
                cmd.Parameters.AddWithValue("@UNIT_PRICE", article.Unit_Price);
                result = cmd.ExecuteNonQuery() == 1;
                t.Commit();
            } catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex}");
                t.Rollback();
            } finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }
    }
}
