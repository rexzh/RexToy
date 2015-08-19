using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest.ORM.SampleEntity.MSSql
{
    class Lifecycle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int StartWeekID { get; set; }
        public int EndWeekID { get; set; }
    }
}
