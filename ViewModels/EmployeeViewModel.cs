using BL.Abstract;
using BL.Impl;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    class EmployeeViewModel
    {
        public List<EmployeeDTO> Employees
        {
            get;
            set;
        }

        public void LoadEmployees()
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();

            IEmployeeService employeeService = new EmployeeService();

            employeeService.Add(new EmployeeDTO() { Id = 1, Name = "Maks", Position = "Senior", BirthDay = new DateTime(2000, 9, 12) });
            employeeService.Add(new EmployeeDTO() { Id = 2, Name = "Igor", Position = "Middle", BirthDay = new DateTime(1999, 7, 8) });
            employeeService.Add(new EmployeeDTO() { Id = 3, Name = "Alex", Position = "Junior", BirthDay = new DateTime(2000, 11, 3) });
            employeeService.Add(new EmployeeDTO() { Id = 4, Name = "Lera", Position = "Trainee", BirthDay = new DateTime(1998, 1, 7) });
            
            employeeService.Save();
            employees = employeeService.GetAll().Data; 

            Employees = employees;
        }
    }
}
