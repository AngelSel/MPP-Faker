
namespace Plugins
{
    public interface IGenerator
    {
    }

    public abstract class Generator<T>:IGenerator
    {
        public abstract T Generate();

    }
}
