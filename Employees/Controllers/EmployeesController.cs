using AutoMapper;
using Employees.Data;
using Employees.Dtos;
using Employees.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Controllers
{

    // Viser hvordan man kommer til de nødvendige ressursene og API endpoints inni controlleren
    // Altså en "base route" til vår controller

    //api/employees
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeesRepo _repository;
        private readonly IMapper _mapper;
        List<Employee> _oEmployees = new List<Employee>();

        public EmployeesController(IEmployeesRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET api/employees
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetAllEmployees()
        {
            var EmployeeItems = _repository.GetAllEmployees();

            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(EmployeeItems));
        }

        // GET api/employees/{id}
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult<EmployeeReadDto> getEmployeeById(int id)
        {
            var employeeItem = _repository.GetEmployeeById(id);
            if (employeeItem != null)
            {
                return Ok(_mapper.Map<EmployeeReadDto>(employeeItem));
            }
            return NotFound();
        }

        // POST api/employees
        [HttpPost]
        public async Task CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            
                var employeeModel = _mapper.Map<Employee>(employeeCreateDto);
                _repository.CreateEmployee(employeeModel);
                _repository.SaveChanges(); // VIKTIG!!

                var employeeReadDto = _mapper.Map<EmployeeReadDto>(employeeModel);

          
            
            
            

        }

     

        // PUT api/employees/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            // Call GET-employee by ID to return the requested resource from the repository
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound();
            }

            // Take data from employee update DTO, that has been supplied by a client then apply it on top of the employee model  
            // USES: EmployeeProfile.cs

            // Uses existing sources, EmployeeUpdateDto (which we map data from) and we want to map to our employee model from the repository
            _mapper.Map(employeeUpdateDto, employeeModelFromRepo);


            // Not necessary, but good practice
            _repository.UpdateEmployee(employeeModelFromRepo);

            // Flushes the changes that has been done back to the database
            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/employees/{id}
        [HttpPatch("{id}")]
        // Gets a patch document from the request                                              HERE
        public ActionResult PartialEmployeeUpdate(int id, JsonPatchDocument<EmployeeUpdateDto> patchDoc)
        {

            // Checks if the resource exists and is up-to-date from the repository (which is a model)
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound();
            }

            // Creates an empty update employee DTO, uses date from repository model then applies the patch
            var employeeToPatch = _mapper.Map<EmployeeUpdateDto>(employeeModelFromRepo);
            patchDoc.ApplyTo(employeeToPatch, ModelState); // The ModelState checks if all the validations are valid
            if (!TryValidateModel(employeeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(employeeToPatch, employeeModelFromRepo);

            // Not necessary, but good practice
            _repository.UpdateEmployee(employeeModelFromRepo);

            // Flushes the changes that has been done back to the database
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/employees/{id}
        [HttpDelete]
        public ActionResult DeleteEmployees(int id)
        {
            // Checks if the resource exists and is up-to-date from the repository (which is a model)
            var employeeModelFromRepo = _repository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteEmployee(employeeModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
         
    }
}
