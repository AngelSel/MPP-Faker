using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    public interface IGenerator
    {
        object Generate(GeneratorContext context);
        bool CanGenerate(Type type);
    }
}
