using System;
using System.Collections.Generic;

namespace UnitTest.Json
{
    class Employee
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        double salary;
        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        DateTime start;
        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }

        private Employee() { }

        public Employee(string name, int age, double salary, DateTime start)
        {
            this.name = name;
            this.age = age;
            this.salary = salary;
            this.start = start;
        }
    }
}
