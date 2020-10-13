using System;

namespace FakerLibrary.Generators.TypesGenerators
{
    class FloatGenerator : Generator<float>
    {
        public override float Generate(GeneratorContext context)
        {
            return (float)context.Random.NextDouble();
        }
    }
}
