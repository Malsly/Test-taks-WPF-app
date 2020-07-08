using BL.Abstract;
using BL.Impl;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Infrastructure;

namespace ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private EmployeeDTO selectedEmployee;
        private ObservableCollection<EmployeeDTO> employees;
        private readonly IEmployeeService employeeService = new EmployeeService();
 
        public ObservableCollection<EmployeeDTO> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        
        public EmployeeDTO SelectedEmployee
        {
            get 
            { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }


        private RelayCommand addEmployee;
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                  (addEmployee = new RelayCommand(obj =>
                  {
                      EmployeeDTO newEmployee = new EmployeeDTO()
                      {
                          Id = SelectedEmployee.Id,
                          Name = SelectedEmployee.Name,
                          Position = SelectedEmployee.Position,
                          BirthDay = SelectedEmployee.BirthDay
                      };

                      if (employeeService.GetAll().Data.Any(item => item.Id == newEmployee.Id))
                          employeeService.Update(newEmployee);
                      else
                      {
                          employeeService.Add(newEmployee);
                      }

                      employeeService.Save();
                      RefreshEmployees();
                  }));
            }
        }

        private RelayCommand saveChangedEmployee;
        public RelayCommand SaveChangedEmployee
        {
            get
            {
                return saveChangedEmployee ??
                  (saveChangedEmployee = new RelayCommand(obj =>
                  {
                      employeeService.Save();
                      RefreshEmployees();
                  }));
            }
        }

        private RelayCommand deleteEmployee;
        public RelayCommand DeleteEmployee
        {
            get
            {
                return deleteEmployee ??
                  (deleteEmployee = new RelayCommand(obj =>
                  {
                      employeeService.Delete(SelectedEmployee.Id);
                      employeeService.Save();
                      RefreshEmployees();
                  }));
            }
        }

        private RelayCommand birthDayChanged;
        public RelayCommand BirthDayChanged
        {
            get
            {
                return birthDayChanged ??
                  (birthDayChanged = new RelayCommand(obj =>
                  {
                      employeeService.Update(SelectedEmployee);
                      RefreshEmployees();
                  }));
            }
        }
        public void RefreshEmployees()
        {
            employees = new ObservableCollection<EmployeeDTO>(employeeService.GetAll().Data);
            OnPropertyChanged(nameof(Employees));
        }
        public void RefreshSelectedEmployee ()
        {
            selectedEmployee = employeeService.Get(selectedEmployee.Id).Data;
            OnPropertyChanged(nameof(SelectedEmployee));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void LoadEmployees()
        {
            ObservableCollection<EmployeeDTO> employees;

            employeeService.Add(new EmployeeDTO() { Id = 1, Name = "Maks", Position = "Senior", BirthDay = new DateTime(2000, 9, 12) });
            employeeService.Add(new EmployeeDTO() { Id = 2, Name = "Igor", Position = "Middle", BirthDay = new DateTime(1999, 7, 8) });
            employeeService.Add(new EmployeeDTO() { Id = 3, Name = "Alex", Position = "Junior", BirthDay = new DateTime(1997, 6, 3) });
            employeeService.Add(new EmployeeDTO() { Id = 4, Name = "Lera", Position = "Trainee", BirthDay = new DateTime(1984, 11, 15) });

            employeeService.Save();

            employees = new ObservableCollection<EmployeeDTO>(employeeService.GetAll().Data);

            Employees = employees;
        }


    }
}
