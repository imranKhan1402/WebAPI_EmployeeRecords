﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_EmployeeRecords.Core.Model.Utility
{
    public class InputParameter
    {
        public string ParameterName
        {
            get;
            set;
        }
        public object ParameterValue
        {
            get;
            set;
        }
        public DbType ParameterType
        {
            get;
            set;
        }
    }
}
