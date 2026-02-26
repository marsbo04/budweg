using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        Employee(int Id, string name) {
            this.Id = Id;
            this.Name = name;
        }
    }
}
