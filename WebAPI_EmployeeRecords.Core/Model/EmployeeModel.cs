using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_EmployeeRecords.Core.Model
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public static EmployeeModel ConvertToModel(DataRow row)
        {
            return new EmployeeModel
            {
                Id = row.Table.Columns.Contains("Id") ? Convert.ToInt32(row["Id"]) : 0,
                FirstName = row.Table.Columns.Contains("FirstName") ? Convert.ToString(row["FirstName"]) : string.Empty,
                MiddleName = row.Table.Columns.Contains("MiddleName") ? Convert.ToString(row["MiddleName"]) : "",
                LastName = row.Table.Columns.Contains("LastName") ? Convert.ToString(row["LastName"]) : "",
            };

        }
    }
}
