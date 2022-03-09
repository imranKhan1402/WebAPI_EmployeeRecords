

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WebAPI_EmployeeRecords.Core.Model.Utility
{
    public class DBContext
    {
        #region Private Property

        private DbProviderFactory dbFactory;
        public string _connectionString;
        private DbConnection _connection;
        private DbDataAdapter _adapter;
        private DbCommand _command;
        private DbParameter _parameter;
        private int _commandTimeOut = 0;

        #endregion

        #region Public Property

        public int CommandTimeOut
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public DBContext(string connectionString, string providerName = "System.Data.SqlClient")
        {
            dbFactory = DbProviderFactories.GetFactory(providerName);
            _connectionString = connectionString;
        }

        public string con()
        {
            return "Data Source=.;Initial Catalog=EmployeeRecords;Integrated Security=true;";
        }
        #endregion

        #region Public Method

        public void Open()
        {
            try
            {
                if (_connection == null)
                {
                    _connection = dbFactory.CreateConnection();
                }
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Open)
                    {
                        _connection.ConnectionString = con();                       
                        _connection.Open();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(@"Server connection failure. Please check your server connection.");
            }
        }
         public void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        public DataTable GetDataTable(string sqlQuery)
        {


            SqlConnection _sqlCon = new SqlConnection(con());
            SqlCommand _sqlCom = null;
            SqlDataAdapter _sqldataAd = null;
            DataTable dataTable = new DataTable();
            try
            {
                _sqlCom = new SqlCommand();
                _sqlCom.CommandType = CommandType.Text;
                _sqlCom.CommandText = sqlQuery;
                _sqlCom.Connection = _sqlCon;
                _sqldataAd = new SqlDataAdapter(_sqlCom);
                _sqldataAd.Fill(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqldataAd.Dispose();
                _sqlCom.Dispose();
                _sqlCon.Close();
            }
            return dataTable;
            
            //_command = dbFactory.CreateCommand();
            //if (_command != null)
            //{
            //    _command.CommandType = CommandType.Text;
            //    _command.CommandText = sqlQuery;
            //    _command.CommandTimeout = _commandTimeOut;
            //    _command.Connection = _connection;
            //}
            //_adapter = dbFactory.CreateDataAdapter();
            //if (_adapter != null)
            //{
            //    _adapter.SelectCommand = _command;
            //    _adapter.Fill(dataTable);
            //}
            //return dataTable;
        }

        public Int32 GetCount(String query, List<InputParameter> inputParameters)
        {
            DataTable dt = new DataTable();
            dt = GetDataTable(query, inputParameters);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows.Count) : 0;

        }

        public string GetSingleString(String query)
        {
            DataTable dt = new DataTable();
            dt = GetDataTable(query);
            return dt.Rows.Count > 0 ? Convert.ToString(dt.Rows[0][0]) : "";

        }
        public Int32 GetSingleInt(String query)
        {
            DataTable dt = new DataTable();
            dt = GetDataTable(query);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;

        }
        public DataTable GetDataTable(string sqlQuery, List<InputParameter> inputParameters)
        {
            
            SqlConnection _sqlCon = new SqlConnection(DatabaseConfiguration.ConnectionString);
            SqlCommand _sqlCom = null;
            SqlDataAdapter _sqldataAd = null;
            DataTable dataTable = new DataTable();
            try
            {
                if (_sqlCon.State == ConnectionState.Open)
                {
                    _sqlCon.Close();
                }
                _sqlCon.Open();
                _sqlCom = new SqlCommand();
                _sqlCom.CommandType = CommandType.Text;
                _sqlCom.CommandText = sqlQuery;
                _sqlCom.Connection = _sqlCon;
                _sqlCom.CommandTimeout = 0;
                for (int i = 0; i < inputParameters.Count; i++)
                {
                    _parameter = dbFactory.CreateParameter();
                    _parameter.DbType = inputParameters[i].ParameterType;
                    _parameter.Value = inputParameters[i].ParameterValue;
                    _parameter.ParameterName = inputParameters[i].ParameterName;
                    _sqlCom.Parameters.Add(_parameter);
                }
                _sqldataAd = new SqlDataAdapter(_sqlCom);
                _sqldataAd.Fill(dataTable);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqldataAd.Dispose();
                _sqlCom.Dispose();
                _sqlCon.Close();
            }
            return dataTable;
            
            //_command = dbFactory.CreateCommand();
            //if (_command != null)
            //{
            //    _command.CommandType = CommandType.Text;
            //    _command.CommandText = sqlQuery;
            //    for (int i = 0; i < inputParameters.Count; i++)
            //    {
            //        _parameter = dbFactory.CreateParameter();
            //        if (_parameter != null)
            //        {
            //            _parameter.DbType = inputParameters[i].ParameterType;
            //            _parameter.Value = inputParameters[i].ParameterValue;
            //            _parameter.ParameterName = inputParameters[i].ParameterName;
            //            _command.Parameters.Add(_parameter);
            //        }
            //    }
            //    _command.CommandTimeout = _commandTimeOut;
            //    _command.Connection = _connection;
            //}
            //_adapter = dbFactory.CreateDataAdapter();
            //if (_adapter != null)
            //{
            //    _adapter.SelectCommand = _command;
            //    _adapter.Fill(dataTable);
            //}
            //return dataTable;
        }
        public string GetSingleString(String query, List<InputParameter> inputParameters)
        {
            DataTable dt = new DataTable();
            dt = GetDataTable(query, inputParameters);
            return dt.Rows.Count > 0 ? Convert.ToString(dt.Rows[0][0]) : "";

        }
        public Int32 GetSingleInt(String query, List<InputParameter> inputParameters)
        {
            DataTable dt = new DataTable();
            dt = GetDataTable(query, inputParameters);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;

        }
        public int ExecuteQuery(string sqlQuery)
        {
           
            _command = dbFactory.CreateCommand();
            int result = 0;
            if (_command != null)
            {
                _command.CommandType = CommandType.Text;
                _command.CommandText = sqlQuery;
                _command.CommandTimeout = _commandTimeOut;
                _command.Connection = _connection;
                result = _command.ExecuteNonQuery();
            }
            return result;
        }
       public int ExecuteQuery(string sqlQuery, List<InputParameter> inputParameters)
        {
            SqlConnection _sqlCon = new SqlConnection(DatabaseConfiguration.ConnectionString);
            SqlCommand _sqlCom = null;
            int result = -1;
           
            try
            {
                if (_sqlCon.State == ConnectionState.Open)
                {
                    _sqlCon.Close();
                }
                _sqlCon.Open();
                _sqlCom = new SqlCommand();
                _sqlCom.CommandType = CommandType.Text;
                _sqlCom.CommandText = sqlQuery;
                _sqlCom.Connection = _sqlCon;
                _sqlCom.CommandTimeout = 0;
                for (int i = 0; i < inputParameters.Count; i++)
                {
                    _parameter = dbFactory.CreateParameter();
                    _parameter.DbType = inputParameters[i].ParameterType;
                    _parameter.Value = inputParameters[i].ParameterValue;
                    _parameter.ParameterName = inputParameters[i].ParameterName;
                    _sqlCom.Parameters.Add(_parameter);
                }
                result = _sqlCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _sqlCom.Dispose();
                _sqlCon.Close();
            }
            return result;
            //_command = dbFactory.CreateCommand();
            //int result = 0;
            //if (_command != null)
            //{
            //    _command.CommandType = CommandType.Text;
            //    _command.CommandText = sqlQuery;
            //    _command.CommandTimeout = _commandTimeOut;
            //    _command.Connection = _connection;

            //    for (int i = 0; i < inputParameters.Count; i++)
            //    {
            //        _parameter = dbFactory.CreateParameter();
            //        if (_parameter != null)
            //        {
            //            _parameter.DbType = inputParameters[i].ParameterType;
            //            _parameter.Value = inputParameters[i].ParameterValue;
            //            _parameter.ParameterName = inputParameters[i].ParameterName;
            //            _command.Parameters.Add(_parameter);
            //        }
            //    }
            //    result = _command.ExecuteNonQuery();
            //}
            //return result;


        }








        public void BeginTransaction()
        {
           
            _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _command.Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _command.Transaction.Rollback();
        }

        public void DisposeTransaction()
        {
            _command.Transaction.Dispose();
        }






        #endregion
    }
}
