using AutoMapper;
using Employees.Dtos;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Profiles
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            // Source  -> Target
            // Employee -> ReadDto
            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeUpdateDto>();
        }
    }
}
