using HR.Department.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace HR.Department.DAL
{
    public class EmployeePersister
    {

        public string File { get; set; }

        private void _ensureFile()
        {
            if (!System.IO.File.Exists(File))
                using (var created = System.IO.File.Create(File))
                {
                    using (var sw = new StreamWriter(created))
                    {
                        var list = new List<Employee> { };
                        _serialize(sw, list);
                    }
                }
        }

        private void _persist(List<Employee> employees)
        {
            using (var sw = new StreamWriter(File, false))
            {
                _serialize(sw, employees);
            }
        }

        public void _serialize(StreamWriter sw, List<Employee> employees)
        {
            var serializer = new JavaScriptSerializer();
            sw.Write(serializer.Serialize(employees));
        }

        public List<Models.Entities.Employee> Get()
        {
            _ensureFile();
            using (var fs = new FileStream(File, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    var data = sr.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    return serializer.Deserialize<List<Employee>>(data);
                }
            }
        }

        public Employee Set(Employee employee )
        {
            var employees = Get();
            if (employee.Id == 0)
            {
                var maxId = employees.Count == 0 ? 0 : employees.Max(e => e.Id);
                employee.Id = maxId + 1;
                employees.Add(employee);
            }
            else
            {
                var oldEmployee = employees.Where(e => e.Id == employee.Id).SingleOrDefault();
                employees.Remove(oldEmployee);
                employees.Add(employee);
            }
            _persist(employees);
            return employee;

        }

        public void Delete(Employee employee)
        {
            var employees = Get();
            var employeeToDelete = employees.Where(e => e.Id == employee.Id).SingleOrDefault();
            employees.Remove(employeeToDelete);
            _persist(employees);

        }

         

        }
}

