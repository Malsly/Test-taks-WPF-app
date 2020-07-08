using BL.Abstract;
using BL.Impl;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Tracing;
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

            employeeService.Add(new EmployeeDTO() { Id = 1, Name = "James", Position = "Senior", BirthDay = new DateTime(2000, 9, 12), IsWorking = false });
            employeeService.Add(new EmployeeDTO() { Id = 2, Name = "Harper", Position = "Middle", BirthDay = new DateTime(1999, 7, 8), IsWorking = true });
            employeeService.Add(new EmployeeDTO() { Id = 3, Name = "Mason", Position = "Junior", BirthDay = new DateTime(1997, 6, 3), IsWorking = true });
            employeeService.Add(new EmployeeDTO() { Id = 4, Name = "Evelyn", Position = "Senior", BirthDay = new DateTime(1988, 1, 1), IsWorking = false });
            employeeService.Add(new EmployeeDTO() { Id = 5, Name = "Ella", Position = "Middle", BirthDay = new DateTime(1981, 8, 14), IsWorking = false });
            employeeService.Add(new EmployeeDTO() { Id = 6, Name = "Avery", Position = "Junior", BirthDay = new DateTime(1984, 3, 21), IsWorking = true });


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
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        //Commands
        private RelayCommand addEmployee;
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                  (addEmployee = new RelayCommand(obj =>
                  {
                      EmployeeDTO newEmployee = new EmployeeDTO();
                      
                      if (selectedEmployee != null)
                        newEmployee = new EmployeeDTO()
                        {
                            Id = SelectedEmployee.Id,
                            Name = SelectedEmployee.Name,
                            Position = SelectedEmployee.Position,
                            BirthDay = SelectedEmployee.BirthDay,
                            IsWorking = SelectedEmployee.IsWorking
                        };
                      
                      if (employeeService.GetAll().Data.Any(item => item.Id == newEmployee.Id))
                          employeeService.Update(newEmployee);
                      else
                          employeeService.Add(newEmployee);

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
                      employeeService.Update(selectedEmployee);
                      employeeService.Save();
                      RefreshEmployees();
                      selectedEmployee = new EmployeeDTO();
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
        //Update props from DB
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
