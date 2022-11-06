using Demo_Core_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Demo_Core_Api.service
{
    public class EmployeeService
    {
        SqlConnection con;
        public EmployeeService(SqlConnection sqlConnection)
        {
            con = sqlConnection;
        }
        public IList<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Employee";
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    employees.Add(new Employee
                    {
                        EmpId = Convert.ToInt32(sdr["EmpId"]),
                        EmpName = Convert.ToString(sdr["EmpName"]),
                        EmpAge = Convert.ToInt32(sdr["EmpAge"])
                    });
                }
            }
            finally
            {
                con.Close();
            }
            return employees;
        }
        public Boolean AddMethod(Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Employee(EmpName, EmpAge) values('" + employee.EmpName + "'," + employee.EmpAge + ")"; ;
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public Boolean UpdateMethod(int EmpId, Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update Employee Set Name='" + employee.EmpName + "', EmpAge=" + employee.EmpAge + "Where(EmpId=" + EmpId + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public Boolean DeleteMethod(int EmpId)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from Employee Where(EmpID=" + EmpId + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

