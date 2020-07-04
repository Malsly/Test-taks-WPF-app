using BL.Abstract;
using BL.Impl;
using DAL_Implementation;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Test_taks_WPF_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IEmployeeService employeeService = new EmployeeService();
            employeeService.Add(new EmployeeDTO() { Id = 1, Name = "65", Position = "daw", BirthDay = new DateTime(2000, 9, 12) });
            employeeService.Save();
            var data = employeeService.GetAll();
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }
    }
}
