using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_EmployeeRecords.Core.DAL.Interface;
using WebAPI_EmployeeRecords.Core.Model;
using WebAPI_EmployeeRecords.Core.Model.Utility;

namespace WebAPI_EmployeeRecords.Core.DAL.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        public static DBContext _dbContext;
        public EmployeeRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public int DeleteEmployee(int id)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("delete from Employee where Id=" + id + " ");
                return _dbContext.ExecuteQuery(query.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EmployeeModel> GetAllEmployee()
        {
            var Query = new StringBuilder();
            Query.Append(" select * from Employee");
            var data = _dbContext.GetDataTable(Query.ToString());
            return from DataRow row in data.Rows select EmployeeModel.ConvertToModel(row);
        }

        public int SaveEmployee(EmployeeModel employeeModel)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("insert into Employee (FirstName,MiddleName,LastName) values (");
                query.Append("'" + employeeModel.FirstName + "',");
                query.Append("'" + employeeModel.MiddleName + "',");
                query.Append("'" + employeeModel.LastName + "')");
                return _dbContext.ExecuteQuery(query.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("update Employee set ");
                query.Append("FirstName='" + employeeModel.FirstName + "',");
                query.Append("MiddleName='" + employeeModel.MiddleName + "',");
                query.Append("LastName='" + employeeModel.LastName + "'");
                query.Append(" where Id=" + employeeModel.Id + "");
                return _dbContext.ExecuteQuery(query.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
