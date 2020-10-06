using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLibrary.Generators
{
    public abstract class Generator<T>
    {
        public abstract T Generate();

    }
}
