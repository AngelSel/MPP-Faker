using System;

namespace FakerLibrary.Generators.TypesGenerators
{
    class ShortGenerator : Generator<short>
    {
        public override short Generate(GeneratorContext context)
        {
            return (short)context.Random.Next(2, 255);
        }
    }
}
