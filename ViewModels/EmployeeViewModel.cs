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
    public class EmployeeViewModel
    {
        public ObservableCollection<EmployeeDTO> Employees
        {
            get;
            set;
        }
        public EmployeeDTO SelectedEmployee
        {
            get;
            set;
        }

        public void LoadEmployees()
        {
            ObservableCollection<EmployeeDTO> employees;

            IEmployeeService employeeService = new EmployeeService();

            employeeService.Add(new EmployeeDTO() { Id = 1, Name = "Maks", Position = "Senior", Age = 19 });
            employeeService.Add(new EmployeeDTO() { Id = 2, Name = "Igor", Position = "Middle", Age = 20 });
            employeeService.Add(new EmployeeDTO() { Id = 3, Name = "Alex", Position = "Junior", Age = 19 });
            employeeService.Add(new EmployeeDTO() { Id = 4, Name = "Lera", Position = "Trainee", Age = 20 });
            
            employeeService.Save();
            employees = new ObservableCollection<EmployeeDTO>(employeeService.GetAll().Data);

            
            Employees = employees;
        }
    }
}
