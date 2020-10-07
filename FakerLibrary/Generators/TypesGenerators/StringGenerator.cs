using Plugins;
using System;
using System.Text;

namespace FakerLibrary.Generators.TypesGenerators
{
    class StringGenerator : Generator<string>
    {
        private Random rand = new Random();
        public override string Generate()
        {
            byte[] tmp = new byte[rand.Next(15) * 2];
            rand.NextBytes(tmp);
            return Encoding.UTF8.GetString(tmp);
        }
    }
}
