using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSettings.Models
{
    public enum Gender
    {
        None,
        Male,
        Female
    }

    class Person
    {
        public Person()
        {
        }

        public Person(int id, string name, Gender gender)
        {
            Id = id;
            Name = name;
            Gender = gender;
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
    }
}
