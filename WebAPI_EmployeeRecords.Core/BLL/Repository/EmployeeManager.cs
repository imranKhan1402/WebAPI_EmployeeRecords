using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_EmployeeRecords.Core.BLL.Interface;
using WebAPI_EmployeeRecords.Core.DAL.Interface;
using WebAPI_EmployeeRecords.Core.DAL.Repository;
using WebAPI_EmployeeRecords.Core.Model;
using WebAPI_EmployeeRecords.Core.Model.Utility;

namespace WebAPI_EmployeeRecords.Core.BLL.Repository
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _iEmployeeRepository;
        private readonly DBContext _dbContext;

        public EmployeeManager()
        {
            _dbContext = new DBContext(DatabaseConfiguration.ConnectionString);
            _iEmployeeRepository = new EmployeeRepository(_dbContext);
        }

        

        public IEnumerable<EmployeeModel> GetAllEmployee()
        {
            try
            {
                _dbContext.Open();
                var DataList = _iEmployeeRepository.GetAllEmployee();
                return DataList;
            }
            catch
            {
                return null;
            }
            finally
            {
                _dbContext.Close();
            }
        }

        public int SaveEmployee(EmployeeModel employeeModel)
        {
            int count = 0;
            try
            {
                _dbContext.Open();
                count= _iEmployeeRepository.SaveEmployee(employeeModel);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _dbContext.Close();
            }
            return count;
        }

        public int UpdateEmployee(EmployeeModel employeeModel)
        {
            int count = 0;
            try
            {
                _dbContext.Open();
                count = _iEmployeeRepository.UpdateEmployee(employeeModel);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _dbContext.Close();
            }
            return count;
        }
        public int DeleteEmployee(int id)
        {
            int count = 0;
            try
            {
                _dbContext.Open();
                count = _iEmployeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _dbContext.Close();
            }
            return count;
        }
    }
}
