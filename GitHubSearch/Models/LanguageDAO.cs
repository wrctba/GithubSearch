using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GitHubSearch.Models
{
    public class LanguageDAO
    {
        private readonly string connectionString = string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["GithubSearchDB"].ToString());

        public List<Language> ListSearch(int searchId)
        {
            List<Language> lstLanguage = new List<Language>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select Item.language as name from SearchItem, Item  where SearchItem.ItemId = Item.id AND SearchItem.SearchId = @searchId ORDER BY Item.language ASC", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@searchId", searchId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Language language = new Language();
                    language.Name = rdr["name"].ToString();
                    lstLanguage.Add(language);
                }
                con.Close();
            }
            return lstLanguage;

        }
        public List<Language> GetAll(bool? justActive = true)
        {
            List<Language> lstLanguage = new List<Language>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT name, active FROM Language WHERE ((active = 1 AND @justActive = 1) OR @justActive = 0) ORDER BY Name ASC", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@justActive", justActive);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Language language = new Language();

                    language.Name = rdr["name"].ToString();
                    language.IsActive = Convert.ToBoolean(rdr["active"]);
                    lstLanguage.Add(language);
                }
                con.Close();
            }
            return lstLanguage;
        }

        public Language Get(int id)
        {
            Language language = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT name, active FROM Language WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    language = new Language();
                    language.Name = rdr["name"].ToString();
                    language.IsActive = Convert.ToBoolean(rdr["active"]);
                }
                rdr.Close();
                con.Close();
            }
            return language;
        }
    }
}