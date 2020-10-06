using System;
using Plugins;

namespace FakerLibrary.Generators.TypesGenerators
{
    class ShortGenerator : Generator<short>
    {
        private Random rand = new Random();
        public override short Generate()
        {
            return (short)rand.Next(2, 255);
        }
    }
}
