using System;
using System.Collections.Generic;

using RexToy.ORM;
using RexToy.ORM.QueryModel;

namespace UnitTest.ORM.SampleEntity.MSSql
{
    class MSSqlSchema
    {
        public MSSqlSchema()
        {
            _person = new View<Person>();
            _contactInfo = new View<ContactInfo>();
            _course = new View<Course>();
            _coursePerson = new View<CoursePerson>();
            _employee = new View<Employee>();
            _week = new View<Week>();
            _lifecycle = new View<Lifecycle>();
        }

        private View<Person> _person;
        public View<Person> Person
        {
            get { return _person; }
        }

        private View<ContactInfo> _contactInfo;
        public View<ContactInfo> ContactInfo
        {
            get { return _contactInfo; }
        }

        private View<Course> _course;
        public View<Course> Course
        {
            get { return _course; }
        }

        private View<CoursePerson> _coursePerson;
        public View<CoursePerson> CoursePerson
        {
            get { return _coursePerson; }
        }

        private View<Employee> _employee;
        public View<Employee> Employee
        {
            get { return _employee; }
        }

        private View<Lifecycle> _lifecycle;
        public View<Lifecycle> Lifecycle
        {
            get { return _lifecycle; }
        }

        private View<Week> _week;
        public View<Week> Week
        {
            get { return _week; }
        }
    }
}