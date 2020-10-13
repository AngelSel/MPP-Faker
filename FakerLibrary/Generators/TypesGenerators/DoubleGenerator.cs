using System;

namespace FakerLibrary.Generators.TypesGenerators
{
    class DoubleGenerator : Generator<double>
    {
        public override double Generate(GeneratorContext context)
        {
            return context.Random.NextDouble();
        }
    }
}
