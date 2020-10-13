using System;
using Plugins;

namespace IntegerGenerator
{
    public class IntgGen : Generator<int>
    {
        private Random rand = new Random();

        public override int Generate()
        {
            return rand.Next();
        }
    }
}
