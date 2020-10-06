using System;
using Plugins;

namespace CharGenerator
{
    public class CharGen : PluginsGenerator<char>
    {
        private Random rand = new Random();

        public override string PluginName => "Char symbol generator";

        public override char Generate()
        {
            return (char)rand.Next(255);
        }
    }
}
