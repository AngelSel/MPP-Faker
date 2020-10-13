using System;

namespace FakerLibrary.Generators.TypesGenerators
{
   
    public class BoolGenerator: Generator<bool>
    {
        public override bool Generate(GeneratorContext context)
        {
            if (context.Random.Next(2) == 0)
                return false;
            else
                return true;
        }
    }
}
