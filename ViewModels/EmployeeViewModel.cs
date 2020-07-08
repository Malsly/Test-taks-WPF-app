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
using System.Windows.Data;
using ViewModels.Infrastructure;

namespace ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private EmployeeDTO selectedEmployee = new EmployeeDTO();
        private ICollectionView employees;
        private readonly IEmployeeService employeeService = new EmployeeService();

        public EmployeeViewModel() 
        {
            IList<EmployeeDTO> employeesFromService;

            employeeService.Add(new EmployeeDTO() { Id = 1, Name = "Maks", Position = "Senior", BirthDay = new DateTime(2000, 9, 12) });
            employeeService.Add(new EmployeeDTO() { Id = 2, Name = "Igor", Position = "Middle", BirthDay = new DateTime(1999, 7, 8) });
            employeeService.Add(new EmployeeDTO() { Id = 3, Name = "Alex", Position = "Junior", BirthDay = new DateTime(1997, 6, 3) });
            employeeService.Add(new EmployeeDTO() { Id = 4, Name = "Lera", Position = "Trainee", BirthDay = new DateTime(1984, 11, 15) });

            employeeService.Save();

            employeesFromService = employeeService.GetAll().Data;
            employees = CollectionViewSource.GetDefaultView(employeesFromService);
        }

        public ICollectionView Employees
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
                      EmployeeDTO newEmployee = new EmployeeDTO();
                      try
                      {
                          if (selectedEmployee != null)
                          {
                              newEmployee = new EmployeeDTO()
                              {
                                  Id = SelectedEmployee.Id,
                                  Name = SelectedEmployee.Name,
                                  Position = SelectedEmployee.Position,
                                  BirthDay = SelectedEmployee.BirthDay
                              };
                          }
                      }
                      catch { }   

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
                      selectedEmployee = new EmployeeDTO();
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
                      selectedEmployee = new EmployeeDTO();
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
            employees = CollectionViewSource.GetDefaultView(employeeService.GetAll().Data);
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

    }
}
