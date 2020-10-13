using System;

namespace FakerLibrary.Generators.TypesGenerators
{
    class LongGenerator : Generator<long>
    {
        public override long Generate(GeneratorContext context)
        {
            return context.Random.Next() << 31 | context.Random.Next();
        }
    }
}
