using System;
using System.Collections.Generic;

namespace UnitTest.ORM.SampleEntity.MySQL
{
    public class Employee : Person
    {
        public int ManagerID { get; set; }
    }
}
