using Microsoft.AspNetCore.Mvc;
using Demo_Core_Api.Models;
using System.Data.SqlClient;
using System.Net.WebSockets;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using Demo_Core_Api.service;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo_Core_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        SqlConnection con;
        EmployeeService Service;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
             con= new SqlConnection(_configuration.GetConnectionString("Company"));
            Service = new EmployeeService(con);
        }


        [HttpGet]
        public JsonResult GetAllEmployee()
        {
            try
            {
                return new JsonResult(Service.GetAll());
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        public Boolean AddEmployee(Employee employee)
        {
            try
            {
                Service.AddMethod(employee);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut]
        public Boolean UpdateEmployee(int EmpId, Employee employee)
        {
            try
            {
                Service.UpdateMethod(EmpId, employee);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpDelete]
        public Boolean DeleteEmployee(int EmpId)
        {
            try
            {
                Service.DeleteMethod(EmpId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}