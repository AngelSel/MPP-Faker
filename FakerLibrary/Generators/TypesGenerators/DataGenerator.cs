using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLibrary.Generators.TypesGenerators
{

    class DataGenerator : Generator<DateTime>
    {
        private Random rand = new Random();

        public override DateTime Generate()
        {
            DateTime startDate = new DateTime(1970, 1, 1);
            int temp = (DateTime.Today - startDate).Days;
            return startDate.AddDays(rand.Next(temp));

        }

    }
}
