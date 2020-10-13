using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    public class GeneratorContext
    {
        public Type TargetType { get; }

        public GeneratorContext(Type targetType)
        {
            TargetType = targetType;
        }
    }
}
