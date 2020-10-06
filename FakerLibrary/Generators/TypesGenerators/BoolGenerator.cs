using System;

namespace FakerLibrary.Generators.TypesGenerators
{
   
    public class BoolGenerator: Generator<bool>
    {
        private Random rand = new Random();

        public override bool Generate()
        {
            if (rand.Next(2) == 0)
                return false;
            else
                return true;
        }
    }
}
