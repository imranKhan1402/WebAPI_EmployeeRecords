using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_EmployeeRecords.Core.BLL.Interface;
using WebAPI_EmployeeRecords.Core.BLL.Repository;
using WebAPI_EmployeeRecords.Core.Model;

namespace WebAPI_EmployeeRecords.Controllers
{
    public class EmployeeController : ApiController
    {
        IEmployeeManager _iEmployeeManager = new EmployeeManager();
        // GET api/<controller>
        [HttpGet]
        public string GetAllEmployee()
        {
            string result = string.Empty;
            try
            {
                var data = _iEmployeeManager.GetAllEmployee();
                result = JsonConvert.SerializeObject(data);
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }
            return result;
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody] EmployeeModel employee)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            try
            {                
                _iEmployeeManager.SaveEmployee(employee);
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return httpResponseMessage;
        }
        [HttpPost]       
        public HttpResponseMessage Edit([FromBody] EmployeeModel employee)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            try
            {                
                _iEmployeeManager.UpdateEmployee(employee);
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return httpResponseMessage;
        }

        
        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            try
            {
                _iEmployeeManager.DeleteEmployee(id);
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK,"Deleted Successfully");                
            }
            catch (Exception ex)
            {

                httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return httpResponseMessage;
        }
    }
}