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

    class ClassA
    {
        public ClassB c { get; set; }
    }
    class ClassB
    {
        public ClassC c { get; set; }
    }
    class ClassC
    {
        public ClassA c { get; set; }
    }


    struct Struct1
    {
        public int field1;
        public char field2;
    }

    class Program
    {
        static void Main(string[] args)
        {
            FakerLibrary.Faker faker = new FakerLibrary.Faker();

            var fakerObject1 = faker.Create<NestedCLass>();
            var fakerObject2 = faker.Create<Struct1>();
            var fakerObject3 = faker.Create<SomeClass>();
            var fakerObject4 = faker.Create<CollClass>();
            var exp1 = faker.Create<ClassA>();
        }
    }
}
