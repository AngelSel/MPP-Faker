using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakerLibrary;
using System;
using System.Collections.Generic;

namespace FakerUnitTests
{
    [TestClass]
    public class UnitTest1
    {

        private static Faker faker;

        struct DefaultSTructure
        {
            public bool field1;
            public char field2;
        }

        struct StructWithOnePublicConstr
        {
            public int field1;
            public StructWithOnePublicConstr(int num1)
            {
                field1 = num1;
            }
        }

        struct StructWithOnePrivateConstr
        {
            int field1;
            public int field2;
            StructWithOnePrivateConstr(int num1, int num2)
            {
                field1 = num1;
                field2 = num2;
            }
        }

        struct StructWithMultipleConstructors
        {
            int field1;
            bool field2;
            public char field3;

            public StructWithMultipleConstructors(int num, bool b)
            {
                field1 = num;
                field2 = b;
                field3 = '1';
            }

            public StructWithMultipleConstructors(int field1, bool field2, char field3)
            {
                this.field1 = field1;
                this.field2 = field2;
                this.field3 = field3;
            }
        }
        class DefClass
        {
            public int field;
            public DateTime field2;
            long field3;
            short field4;
            public bool field5;
        }

        class ClassWithPrivateConstr
        {
            public int field;
            public DateTime field2;
            long field3;
            short field4;
            public bool field5;

            ClassWithPrivateConstr(long l,short sh)
            {
                this.field3 = l;
                this.field4 = sh;
            }
        }
        class MultipleConstrClass
        {
            public int field;
            public DateTime field2;
            long field3;
            short field4;
            public bool field5;

            MultipleConstrClass()
            {

            }

            MultipleConstrClass(long l, short sh)
            {
                this.field3 = l;
                this.field4 = sh;
            }

        }
        class SomeClass
        {
            long field1;
            short field2;

            public SomeClass(long l,short sh)
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

        class MultCtorClass
        {
            public int i;
            public DateTime t;
            long l;
            public char c { get; }
            public bool b { get; }
            double d;

            public MultCtorClass()
            {

            }
            public MultCtorClass(long l, double d)
            {
                this.l = l;
                this.d = d;
            }
        }

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


        [TestMethod]
        public void TestWithDefStruct()
        {
            faker = new Faker();
            var created = faker.Create<StructWithOnePublicConstr>();
            var notExpected = new DefaultSTructure();
            Assert.AreNotEqual(notExpected.field1, created.field1);
        }

        [TestMethod]
        public void TestMultipleConstrStruct()
        {
            faker = new Faker();
            var actual = faker.Create<StructWithMultipleConstructors>();
            var notExpected = new StructWithMultipleConstructors();
            Assert.AreNotEqual(notExpected.field3, actual.field3);

        }

        [TestMethod]
        public void TestDefaultClass()
        {
            faker = new Faker();
            var actual = faker.Create<DefClass>();
            var notExpected = new DefClass();
            Assert.AreNotEqual(notExpected.field, actual.field);
            Assert.AreNotEqual(notExpected.field2, actual.field2);

        }

        [TestMethod]
        public void TestPrivateConstrClass()
        {
            faker = new Faker();
            var actual = faker.Create<ClassWithPrivateConstr>();
            ClassWithPrivateConstr expected = null;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CreateNestedClasses()
        {
            faker = new Faker();
            var actual = faker.Create<NestedCLass>();
            var notExpected = new NestedCLass();
            Assert.AreNotEqual(notExpected.a, actual.a);
            Assert.AreNotEqual(notExpected.sClass,actual.sClass);
        }

        [TestMethod]
        public void CreateMultCtorClass()
        {
            faker = new Faker();
            var actual = faker.Create<MultCtorClass>();
            var notExpected = new MultCtorClass();
            Assert.AreEqual(notExpected.c, actual.c);
            Assert.AreNotEqual(notExpected.i, actual.i);
            Assert.AreNotEqual(notExpected.t, actual.t);
        }

        [TestMethod]
        public void CreateListClass()
        {
            faker = new Faker();
            var actual = faker.Create<CollClass>();
            var notExpected = new CollClass(new List<double>());
            CollectionAssert.AreNotEqual(notExpected.ints, actual.ints);
            CollectionAssert.AreNotEqual(notExpected.times, actual.times);
        }
    }
}
