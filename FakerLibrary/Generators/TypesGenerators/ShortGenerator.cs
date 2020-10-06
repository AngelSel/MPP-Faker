using System;
using System.Collections.Generic;
using System.Text;

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
