using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Repo
{
    internal class RepoEmployeecs
    {
        private List<Models.Employee> employees;


        public void add(Models.Employee employee)
        {
            employees.Add(employee);
        }
        public List<Models.Employee> getAll()
        {
            return employees;
        }
        public Models.Employee getByID(int id)
        {
            return employees.Find(e => e.Id == id);
        }
        public List<Models.Employee> getByName(string name)
        {
            return employees.FindAll(e => e.Name == name);
        }
    }
}
