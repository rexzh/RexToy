using System;
using System.Collections.Generic;
using System.ComponentModel;

using RexToy.DesignPattern;

namespace UnitTest.Sample
{
    class Person : NotifyPropertyChangeBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                this.OnPropertyChange<Person>(p => p.Name);
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (_age == value)
                    return;
                _age = value;
                this.OnPropertyChange<Person>(p => p.Age);
            }
        }
    }
}
