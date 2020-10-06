using System;
using Plugins;

namespace FakerLibrary.Generators.TypesGenerators
{
    class LongGenerator : Generator<long>
    {
        private Random rand = new Random();
        public override long Generate()
        {
            return rand.Next() << 31 | rand.Next();
        }
    }
}
