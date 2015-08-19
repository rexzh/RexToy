using System;
using System.Collections.Generic;

using RexToy.ORM.MappingInfo;

namespace UnitTest.ORM.SampleEntity
{
    public abstract class _Person
    {
        [Column("ID", PrimaryKey = true)]
        public virtual int ID { get; set; }

        [Column("Birth")]
        public virtual DateTime Birth { get; set; }

        [Column("Name", Nullable = false, Length = 128)]
        public virtual string Name { get; set; }

        [Column("Gender", Nullable = false)]
        public virtual bool Gender { get; set; }
    }
}
