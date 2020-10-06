using FakerLibrary.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FakerLibrary
{
    public class Faker
    {

        private Dictionary<Type, IGenerator> generators;
        public T Create<T>() 
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t) 
        {
            if(t.IsPrimitive)
            {
                return generators[t].GetType().InvokeMember("", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, generators[t], null);
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
                Type fieldType = paramsInfo[i].ParameterType;
                object newValue = default;


            }
        }

        private void GenerateFieldsAndProperties(object createdObject)
        {

        }

        private Dictionary<Type,IGenerator> LoadGenerators()
        {
            Dictionary<Type, IGenerator> loadedGenerators = new Dictionary<Type, IGenerator>();
            string pluginsPath = Directory.GetCurrentDirectory() + @"";

            foreach(string name in Directory.GetFiles(pluginsPath,"*.dll"))
            {
                Assembly asm = Assembly.LoadFrom(name);
                foreach(Type t in asm.GetTypes())
                {
                    //проверка на то что существуют такие генераторы
                    var currentGenerator = Activator.CreateInstance(t);
                    loadedGenerators.Add(t.BaseType.GetGenericArguments()[0], (IGenerator)currentGenerator);
                }
            }

            foreach(Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                //if проверка на то, что есть генератор такого типа
                //если да - добавляем его в словарь генераторов
            }

            return loadedGenerators;
        }

    }
}
