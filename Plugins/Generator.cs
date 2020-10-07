
using System;
using System.Collections.Generic;

namespace Plugins
{
    public interface IGenerator
    {
    }

    public abstract class Generator<T>:IGenerator
    {
        public abstract T Generate();

        public List<T> GenerateList()
        {
            Random rand = new Random();
            List<T> createdList = new List<T>();

            for(int i =0;i< rand.Next();i++)
                createdList.Add(Generate());

            return createdList;
        }

    }
}
