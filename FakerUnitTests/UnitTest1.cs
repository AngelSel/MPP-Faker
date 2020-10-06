using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakerLibrary;
using System;

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

        [TestMethod]
        public void TestWithDefStruct()
        {
            faker = new Faker();
            var created = faker.Create<StructWithOnePublicConstr>();
            var notExpected = new DefaultSTructure();
            Assert.AreNotEqual(notExpected.field1, created.field1);
        }

        [TestMethod]
        public void TestStructWithPrivateConstr()
        {
            faker = new Faker();
            var actual = faker.Create<StructWithOnePrivateConstr>();
            var notExpected = new StructWithOnePrivateConstr();
            Assert.AreNotEqual(notExpected.field2, actual.field2);

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
            var Expected = new DefClass();
            Assert.AreEqual(Expected.field, actual.field);
            Assert.AreEqual(Expected.field2, actual.field2);
            Assert.AreEqual(Expected.field5, actual.field5);

        }

        [TestMethod]
        public void TestPrivateConstrClass()
        {
            faker = new Faker();
            var actual = faker.Create<ClassWithPrivateConstr>();
            ClassWithPrivateConstr expected = null;
            Assert.AreEqual(expected, actual);

        }








    }
}
