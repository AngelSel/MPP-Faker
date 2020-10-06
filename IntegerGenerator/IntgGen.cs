using System;
using Plugins;

namespace IntegerGenerator
{
    public class IntgGen : PluginsGenerator<int>
    {
        private Random rand = new Random();

        public override string PluginName => "Integer number generator";

        public override int Generate()
        {
            return rand.Next();
        }
    }
}
