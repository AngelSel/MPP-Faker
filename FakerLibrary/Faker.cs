using System;

namespace FakerLibrary
{
    public class Faker
    {

        public T Create<T>() 
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t) 
        {

        }

    }
}
