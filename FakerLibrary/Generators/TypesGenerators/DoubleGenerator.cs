using System;
using Plugins;

namespace FakerLibrary.Generators.TypesGenerators
{
    class DoubleGenerator : Generator<double>
    {
        private Random rand = new Random();
        public override double Generate()
        {
            return rand.NextDouble();
        }
    }
}
