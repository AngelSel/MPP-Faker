﻿using System;
using System.Collections.Generic;
using System.Text;


namespace Plugins
{
    public class GeneratorContext
    {
        public Random Random { get; }

        public Type TargetType { get; }

        public Faker Faker { get; }

        public GeneratorContext(Type targetType)
        {
            TargetType = targetType;
        }
    }
}
