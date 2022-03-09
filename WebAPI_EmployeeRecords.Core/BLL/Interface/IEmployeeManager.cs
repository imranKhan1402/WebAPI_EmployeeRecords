using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_EmployeeRecords.Core.Model;

namespace WebAPI_EmployeeRecords.Core.BLL.Interface
{
    public interface IEmployeeManager
    {
        int SaveEmployee(EmployeeModel employeeModel);
        int UpdateEmployee(EmployeeModel employeeModel);
        int DeleteEmployee(int id);
        IEnumerable<EmployeeModel> GetAllEmployee();

    }
}
