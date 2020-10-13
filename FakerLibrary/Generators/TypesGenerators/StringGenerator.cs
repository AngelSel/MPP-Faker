using System;
using System.Text;

namespace FakerLibrary.Generators.TypesGenerators
{
    class StringGenerator : Generator<string>
    {
        public override string Generate(GeneratorContext context )
        {
            byte[] tmp = new byte[context.Random.Next(15) * 2];
            context.Random.NextBytes(tmp);
            return Encoding.UTF8.GetString(tmp);
        }
    }
}
