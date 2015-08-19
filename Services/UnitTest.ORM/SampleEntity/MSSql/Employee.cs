using System;
using System.Collections.Generic;

namespace UnitTest.ORM.SampleEntity.MSSql
{
    public class Employee : Person
    {
        public int ManagerID { get; set; }
    }
}
