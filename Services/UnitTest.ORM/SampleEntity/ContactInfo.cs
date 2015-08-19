using System;
using System.Collections.Generic;

namespace UnitTest.ORM.SampleEntity
{
    public abstract class _ContactInfo
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string Phone { get; set; }
        public PhoneType PhoneType { get; set; }
    }
}
