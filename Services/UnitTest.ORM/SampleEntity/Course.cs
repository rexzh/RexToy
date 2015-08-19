using System;

namespace UnitTest.ORM.SampleEntity
{
    public abstract class _Course
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Guid UID { get; set; }
    }
}
