using System;

namespace FakerLibrary.Generators.TypesGenerators
{
    class FloatGenerator : Generator<float>
    {
        private Random rand = new Random();
        public override float Generate()
        {
            return (float)rand.NextDouble();
        }
    }
}
