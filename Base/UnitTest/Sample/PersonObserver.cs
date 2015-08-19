using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnitTest.Sample
{
    public class PersonObserver
    {
        public string ChangedName { get; set; }

        public void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.ChangedName = e.PropertyName;
        }
    }
}
