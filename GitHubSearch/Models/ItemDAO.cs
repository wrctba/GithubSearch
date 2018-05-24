using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GitHubSearch.Models
{
    public class ItemDAO
    {
        private readonly string connectionString = string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["GithubSearchDB"].ToString());

        public void Add(Item item)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO ITEM (id, name, owner, description, language, details) VALUES (@id, @name, @owner, @description, @language, @details)", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@owner", item.Owner);
                cmd.Parameters.AddWithValue("@description", item.Description);
                cmd.Parameters.AddWithValue("@language", item.Language);
                cmd.Parameters.AddWithValue("@details", item.Details);

                con.Open();
                int ret = cmd.ExecuteNonQuery();                
                con.Close();
                if (ret <= 0)
                {
                    throw new Exception("No rows inserted.");
                }
            }
        }

        public void Update(Item item)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE ITEM SET name = @name, owner = @owner, description = @description, language = @language, details = @details WHERE id = @id", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@owner", item.Owner);
                cmd.Parameters.AddWithValue("@description", item.Description);
                cmd.Parameters.AddWithValue("@language", item.Language);
                cmd.Parameters.AddWithValue("@details", item.Details);

                con.Open();
                int ret = cmd.ExecuteNonQuery();
                con.Close();
                if (ret <= 0)
                {
                    throw new Exception("No rows inserted.");
                }
            }
        }

        public Item Get(int id)
        {
            Item item = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Item WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    item = new Item();
                    item.Id = Convert.ToInt32(rdr["id"]);
                    item.Name = rdr["name"].ToString();
                    item.Owner = rdr["owner"].ToString();
                    item.Description = rdr["description"].ToString();
                    item.Language = rdr["language"].ToString();
                    item.Details = rdr["details"].ToString();
                }
                rdr.Close();
                con.Close();
            }
            return item;
        }
    }
}