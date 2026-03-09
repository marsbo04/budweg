using dimvetral.Models;
using System.Collections.Generic;

namespace dimvetral.Models.Repo
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        List<Employee> GetAll();
        List<Employee> GetByName(string name);
        List<Employee> GetByStation(int stationId);
        void Add(Employee employee);
    }
}