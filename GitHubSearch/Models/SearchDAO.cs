using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace GitHubSearch.Models
{
    public class SearchDAO
    {
        private readonly string connectionString = string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["GithubSearchDB"].ToString());
        public static int PageSize { get; } = 20;

        public List<Search> GetAll(int page, out int count)
        {
            List<Search> lstSearch = new List<Search>();
            count = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT *, count = COUNT(*) OVER() FROM Search ORDER BY date DESC OFFSET @PageSize * (@PageNumber - 1) ROWS FETCH NEXT @PageSize ROWS ONLY; ", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.AddWithValue("@PageNumber", page);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Search search = new Search();

                    search.Id = Convert.ToInt32(rdr["id"]);
                    search.Date = Convert.ToDateTime(rdr["date"]);
                    search.Asp = Convert.ToBoolean(rdr["asp"]);
                    search.Php = Convert.ToBoolean(rdr["php"]);
                    search.C = Convert.ToBoolean(rdr["c"]);
                    search.Java = Convert.ToBoolean(rdr["java"]);
                    search.Python = Convert.ToBoolean(rdr["python"]);
                    count  = Convert.ToInt32(rdr["count"]);
                    lstSearch.Add(search);
                }
                con.Close();
            }
            return lstSearch;
        }

        public void Add(Search search)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Search (date, asp, php, c, java, python) OUTPUT INSERTED.ID VALUES (@date, @asp, @php, @c, @java, @python)", con);
                cmd.CommandType = CommandType.Text;

                search.Date = DateTime.Now;
                cmd.Parameters.AddWithValue("@date", search.Date);
                cmd.Parameters.AddWithValue("@asp", search.Asp);
                cmd.Parameters.AddWithValue("@php", search.Php);
                cmd.Parameters.AddWithValue("@c", search.C);
                cmd.Parameters.AddWithValue("@java", search.Java);
                cmd.Parameters.AddWithValue("@python", search.Python);

                con.Open();
                search.Id = (int)cmd.ExecuteScalar();
                if (search.Id <= 0)
                {
                    con.Close();
                    throw new Exception("No rows inserted.");
                } else
                {
                    ItemDAO itemDAO = new ItemDAO();
                    if (search.Items != null)
                    {
                        foreach (Item item in search.Items) {
                            if (itemDAO.Get(item.Id) == null)
                            {
                                itemDAO.Add(item);
                            } else
                            {
                                itemDAO.Update(item);
                            }
                            cmd = new SqlCommand("INSERT INTO SearchItem (SearchId, ItemId) VALUES (@SearchId, @ItemId)", con);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@SearchId", search.Id);
                            cmd.Parameters.AddWithValue("@ItemId", item.Id);
                            int ret = cmd.ExecuteNonQuery();
                            if (ret <= 0)
                            {
                                con.Close();
                                throw new Exception("No rows inserted.");
                            }
                        }
                    }
                }
                con.Close();
            }
        }

        public Search Get(int id)
        {
            Search search = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Search WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    search = new Search();
                    search.Id = Convert.ToInt32(rdr["id"]);
                    search.Asp = Convert.ToBoolean(rdr["asp"]);
                    search.C = Convert.ToBoolean(rdr["c"]);
                    search.Php  = Convert.ToBoolean(rdr["php"]);
                    search.Java = Convert.ToBoolean(rdr["java"]);
                    search.Python = Convert.ToBoolean(rdr["python"]);
                }
                rdr.Close();

                if (search != null)
                {
                    sqlQuery = "SELECT ItemId FROM SearchItem WHERE SearchId = @id";
                    cmd = new SqlCommand(sqlQuery, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@id", search.Id);

                    rdr = cmd.ExecuteReader();                    

                    while (rdr.Read())
                    {
                        ItemDAO itemDAO  = new ItemDAO();
                        Item item = itemDAO.Get(Convert.ToInt32(rdr["ItemId"]));
                        if (search.Items == null)
                            search.Items = new List<Item>();
                        search.Items.Add(item);
                    }
                    rdr.Close();
                }
                con.Close();
            }
            return search;
        }

        public bool Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE SearchItem WHERE SearchId = @id; DELETE Search WHERE id = @id", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                int ret = cmd.ExecuteNonQuery();
                con.Close();
                if (ret <= 0)
                {
                    throw new Exception("No rows deleted.");
                }
                return true;
            }
        }
    }
}