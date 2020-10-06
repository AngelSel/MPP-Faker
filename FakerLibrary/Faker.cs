using Plugins;
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

        public Faker()
        {
            this.generators = LoadGenerators();
        }

        public T Create<T>() 
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t) 
        {
            if(t.IsPrimitive || t == typeof(DateTime))
            {
                return generators[t].GetType().InvokeMember("Generate", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, generators[t], null);
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
            object createdClassObject = default;
            if (currentConstructors.Length == 0)
            {
                // createdClassObject = Activator.CreateInstance(type);
                // return createdClassObject;
                return default;
            }
            object[] constructorParams = null;
            ConstructorInfo chosenConstructor = null;

            
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
            ConstructorInfo[] currentConstructors = type.GetConstructors();
            object createdStructure = default;
            if (currentConstructors.Length == 0)
            {
                createdStructure = Activator.CreateInstance(type);
                return createdStructure;
            }

            object[] constructorParams = null;
            ConstructorInfo chosenConstructor = null;

            foreach (ConstructorInfo cInfo in currentConstructors.OrderByDescending(c => c.GetParameters().Length))
            {
                constructorParams = GenerateConstructorsParams(cInfo);

                try
                {
                    createdStructure = cInfo.Invoke(constructorParams);
                    chosenConstructor = cInfo;
                }
                catch
                {
                    continue;
                }
            }

            GenerateFieldsAndProperties(createdStructure);
            return createdStructure;
        }

        private object[] GenerateConstructorsParams(ConstructorInfo cInfo)
        {
            ParameterInfo[] paramsInfo = cInfo.GetParameters();
            if (paramsInfo.Length == 0)
                return null;
            List<object> generatedParams = new List<object>();
            object newValue = default;
            foreach(ParameterInfo param in paramsInfo)
            {
                if (generators.TryGetValue(param.ParameterType, out IGenerator g))
                {
                    Type fieldType = param.ParameterType;
                    newValue = generators[fieldType].GetType().InvokeMember("Generate", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, generators[fieldType], null);
                }
                generatedParams.Add(newValue);
            }

            return generatedParams.ToArray();
        }

        private void GenerateFieldsAndProperties(object createdObject)
        {
            Type type = createdObject.GetType();
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();
            foreach (FieldInfo field in fields)
                if (field.GetValue(createdObject) == null)
                    field.SetValue(createdObject, Create(field.FieldType));

            foreach(PropertyInfo property in properties)
            {
                if(property.CanWrite)
                {
                    if (property.CanRead && property.GetValue(createdObject) != null)
                        continue;
                    property.SetValue(createdObject, Create(property.PropertyType));
                }
            }
        }

        private Dictionary<Type,IGenerator> LoadGenerators()
        {
            Dictionary<Type, IGenerator> loadedGenerators = new Dictionary<Type, IGenerator>();

            string pluginsPath = @"d:\Ангелина\5 сем\5 сем\СПП\Lab2-MPP\MPP-Faker\pl";
            string[] f = Directory.GetFiles(pluginsPath, "*.dll");
             foreach ( string name in Directory.GetFiles(pluginsPath, "*.dll"))
             {
                 Assembly asm = Assembly.LoadFrom(name);
                 foreach (Type t in asm.GetTypes())
                 {
                     if (IsRequiredType(t, typeof(PluginsGenerator<>)))
                     {
                         var currentGenerator = Activator.CreateInstance(t);
                         loadedGenerators.Add(t.BaseType.GetGenericArguments()[0], (IGenerator)currentGenerator);
                     }
                 }
            } 

            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(IsRequiredType(t, typeof(Generator<>)))
                    loadedGenerators.Add(t.BaseType.GetGenericArguments()[0], (IGenerator)Activator.CreateInstance(t));
            }

            //string pluginsPath = Directory.GetCurrentDirectory() + @"d:/Ангелина/5 сем/5 сем/СПП/Lab2-MPP/MPP-Faker/pl";
          

            return loadedGenerators;
        }

        private bool IsRequiredType(Type current, Type isRequired)
        {
            while(current!=null && current!=typeof(object))
            {
                Type currType = current.IsGenericType ? current.GetGenericTypeDefinition() : current;
                if (isRequired == currType)
                    return true;
                current = current.BaseType;
            }
            return false;
        }
    }
}
