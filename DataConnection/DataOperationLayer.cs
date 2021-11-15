using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConnection
{
    public class DataOperationLayer
    {
		public string _ConnectionString { get; set; }

		public DataOperationLayer(string connectionString)
		{
			_ConnectionString = connectionString;
		}

		public SqlConnection CreateConnection()
		{
			SqlConnection val = new SqlConnection();
			((DbConnection)val).ConnectionString = _ConnectionString;
			return val;
		}

		public string callStoredProcedure(string sp_name, NameValueCollection nv)
		{
			SqlConnection connection = CreateConnection();
			SqlCommand command = new SqlCommand();
			command.Connection = connection;

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = sp_name;

			foreach (string key in nv)
			{
				command.Parameters.AddWithValue(key, nv[key]);


			}
			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			dataAdapter.SelectCommand = command;
			DataSet dataSet = new DataSet();

			dataAdapter.Fill(dataSet);



			string json = "";
			if (dataSet.Tables.Count > 0)
			{
				for (int i = 0; i < dataSet.Tables.Count; i++)
				{
					for (int j = 0; j < dataSet.Tables[i].Rows.Count; j++)
					{
						json += dataSet.Tables[i].Rows[j][0].ToString();
					}
				}
			}
			else
			{
				json = "no";
			}
			return json;
		}

		public DataTable List(string sp_name, NameValueCollection nv)
		{
			SqlConnection connection = CreateConnection();
			SqlCommand command = new SqlCommand();
			command.Connection = connection;

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = sp_name;




			foreach (string key in nv)
			{
				command.Parameters.AddWithValue(key, nv[key]);


			}

			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			dataAdapter.SelectCommand = command;
			DataTable dt = new DataTable();
			dataAdapter.Fill(dt);


			return dt;
		}

		public DataSet Statusget(string sp_name, NameValueCollection nv)
		{
			
            SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sp_name;




            foreach (string key in nv)
            {
                command.Parameters.AddWithValue(key, nv[key]);


            }

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);


            return dataSet;
		}

		public DataSet AutheticateUser(string sp_name, NameValueCollection nv)
		{
			SqlConnection connection = CreateConnection();
			SqlCommand command = new SqlCommand();
			command.Connection = connection;

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = sp_name;




			foreach (string key in nv)
			{
				command.Parameters.AddWithValue(key, nv[key]);


			}

			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			dataAdapter.SelectCommand = command;
			DataSet dataSet = new DataSet();

			dataAdapter.Fill(dataSet);


			return dataSet;

		}
	}
}
