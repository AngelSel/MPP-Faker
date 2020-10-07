using System;
using System.Collections.Generic;
using FakerLibrary;

namespace Faker
{
    class CollClass
    {
        public List<int> ints;
        List<double> doubles;
        List<char> chars;
        public List<DateTime> times;

        public CollClass(List<double> doubles)
        {
            this.doubles = doubles;
        }
    }

    class SomeClass
    {
        long field1;
        short field2;

        public SomeClass(long l, short sh)
        {
            field1 = l;
            field2 = sh;
        }
    }


    class NestedCLass
    {
        public int a;
        public SomeClass sClass;

        NestedCLass(SomeClass sc)
        {
            this.sClass = sc;
        }

        public NestedCLass()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FakerLibrary.Faker faker = new FakerLibrary.Faker();
            //var actual = faker.Create<CollClass>();
            //var notExpected = new CollClass(new List<double>());

            var actual = faker.Create<NestedCLass>();
            var notExpected = new NestedCLass();

        }
    }
}
