﻿using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Data
{
    public class SqlEmployeesRepo : IEmployeesRepo
    {
        private readonly EmployeeContext _context;

        public SqlEmployeesRepo(EmployeeContext context)
        {
            _context = context;
        }


        // CREATE for å lage en ny employee
        public void CreateEmployee(Employee cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Employees.Add(cmd);
        }

        // DELETE for å slette employee
        public void DeleteEmployee(Employee cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Employees.Remove(cmd);
        }


        // GET uten ID som henter fram alle employees
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
        
        // GET for å hente employee via ID
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(p => p.Id == id);
        }

        // Lagrer endringene mot databasen
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


        // PUT funksjon
        public void UpdateEmployee(Employee cmd)
        {
            // Nothing
        }
    }
}
