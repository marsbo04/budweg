using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int staionId { get; set; }
        Employee(int Id, string name) {
            this.Id = Id;
            this.Name = name;
        }
    }
}
