
using System;
using System.Collections.Generic;

namespace FakerLibrary
{
    public abstract class Generator<T> : IGenerator
    {
        public abstract T Generate(GeneratorContext context);
        object IGenerator.Generate(GeneratorContext context)
        {
           return Generate(context);
        }

        bool IGenerator.CanGenerate(Type type)
        {
            return type == typeof(T);
        }
    }

}
