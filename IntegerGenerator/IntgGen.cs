using FakerLibrary;

namespace IntegerGenerator
{
    public class IntgGen : Generator<int>
    {
        public override int Generate(GeneratorContext context)
        {
            return context.Random.Next(1,10000);
        }
    }
}
