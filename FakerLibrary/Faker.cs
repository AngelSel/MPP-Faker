using System;
using System.Linq;
using System.Reflection;

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
            if(t.IsPrimitive)
            {
                return CreatePrimitive(t);
            }
            else if(t.IsClass)
            {
                return CreateClassObject(t);

            }
            else if(t.IsValueType)
            {
                return CreateStructure(t);
            }
            throw new Exception("Cant create new object");
        }

        private object CreatePrimitive(Type type)
        {

        }

        private object CreateClassObject(Type type)
        {
            ConstructorInfo[] currentConstructors = type.GetConstructors();
            object[] constructorParams = null;
            ConstructorInfo chosenConstructor = null;
            object createdClassObject = default;
            
            foreach (ConstructorInfo cInfo in currentConstructors.OrderByDescending(c => c.GetParameters().Length))
            {
                constructorParams = GenerateConstructorsParams(cInfo);
                try
                {
                    createdClassObject = cInfo.Invoke(constructorParams);
                    chosenConstructor = cInfo;
                }
                catch
                {
                    continue;
                }
            }

            GenerateFieldsAndProperties(createdClassObject);
            return createdClassObject;
        }
        private object CreateStructure(Type type)
        {
           
        }

        private object[] GenerateConstructorsParams(ConstructorInfo cInfo)
        {
            ParameterInfo[] paramsInfo = cInfo.GetParameters();
            object[] generatedParams = new object[paramsInfo.Length];

            for(int i=0; i < generatedParams.Length; i++)
            {

            }
        }

        private void GenerateFieldsAndProperties(object createdObject)
        {

        }

    }
}
