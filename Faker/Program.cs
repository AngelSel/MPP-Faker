using System;
using FakerLibrary;

namespace Faker
{
    public struct testStrustWIthoutConstructor
    {
        public short n;
        public short B { set; get; }
    }

    public struct testStructWithConstructor
    {
        public int x;
        private int y;
        public short z;
        public char c;

        public testStructWithConstructor(int num1, int num2, short num3, char c1)
        {
            x = num1;
            y = num2;
            z = num3;
            c = c1;
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
    struct StructWithOnePrivateConstr
    {
        int field1;
        public int field2;
        StructWithOnePrivateConstr(int field)
        {
            field1 = field;
            field2 = 1;
        }
    }
    class ClassWithPrivateConstr
    {
        public int field;
        public DateTime field2;
        long field3;
        short field4;
        public bool field5;

        ClassWithPrivateConstr(long l, short sh)
        {
            this.field3 = l;
            this.field4 = sh;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FakerLibrary.Faker faker = new FakerLibrary.Faker();
            var actual = faker.Create<ClassWithPrivateConstr>();
            ClassWithPrivateConstr expected = null;

        }
    }
}
