using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Data
{
   public interface IEmployeesRepo
    {
        bool SaveChanges();

        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int Id);

        void CreateEmployee(Employee cmd);
        void UpdateEmployee(Employee cmd);
        void DeleteEmployee(Employee cmd);
    }
}
