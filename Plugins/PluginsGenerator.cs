using System;

namespace Plugins
{
    public abstract class PluginsGenerator<T> : Generator<T>, IPlugin
    {
        public abstract string PluginName { get; }
    }
}
