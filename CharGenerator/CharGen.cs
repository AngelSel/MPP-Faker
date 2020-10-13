using System;
using Plugins;

namespace CharGenerator
{
    public class CharGen : Generator<char>
    {
        private Random rand = new Random();

        public override char Generate()
        {
            return (char)rand.Next(255);
        }
    }
}
