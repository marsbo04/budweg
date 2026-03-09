using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    public class Building
    {
        public string Name { get; set; }
        public string Section { get; set; }

        public Building(string name, string section)
        {
            Name = name;
            Section = section;
        }

        public string fullName()
        {
            return string.IsNullOrEmpty(Section) 
                ? Name 
                : $"{Name} - {Section}";
        }
    }
}