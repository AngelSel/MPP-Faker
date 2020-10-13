using System;
using FakerLibrary;

namespace CharGenerator
{
    public class CharGen : Generator<char>
    {
        public override char Generate(GeneratorContext context)
        {
            return (char)context.Random.Next(255);
        }
    }
}
