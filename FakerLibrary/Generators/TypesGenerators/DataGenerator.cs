using System;

namespace FakerLibrary.Generators.TypesGenerators
{

    class DataGenerator : Generator<DateTime>
    {
        public override DateTime Generate(GeneratorContext context)
        {
            DateTime startDate = new DateTime(1970, 1, 1);
            int temp = (DateTime.Today - startDate).Days;
            return startDate.AddDays(context.Random.Next(temp));

        }

    }
}
