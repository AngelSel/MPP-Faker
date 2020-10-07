using Plugins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FakerLibrary
{
    public class Faker
    {

        private Dictionary<Type, IGenerator> generators;
        public int MaxCircularDependency { get; set; } = 0;
        public int currentCircularDependency = 0;
        public Stack<Type> constructionStack = new Stack<Type>();


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
            if(t.IsPrimitive || t == typeof(DateTime) || t == typeof(string))
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
            else if(t.IsGenericType)
            {
                Type[] temp = t.GetGenericArguments();
                return generators[temp[0]].GetType().InvokeMember("GenerateList", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, generators[temp[0]], null);
            }
            throw new Exception("Cant create new object");
        }

        private object CreateClassObject(Type type)
        {
            ConstructorInfo[] currentConstructors = type.GetConstructors();
            object createdClassObject = default;

            if (currentConstructors.Length == 0 || ((currentCircularDependency = constructionStack.Where(t => t.Equals(type)).Count()) > MaxCircularDependency))
                return default;
            constructionStack.Push(type);

            object[] constructorParams = null;
            ConstructorInfo chosenConstructor = null;
            bool isCreated = true;
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
                    isCreated = false;
                    continue;
                }
                if (isCreated)
                    break;
            }

            GenerateFieldsAndProperties(createdClassObject,constructorParams,chosenConstructor);
            return createdClassObject;
        }

        private object CreateStructure(Type type)
        {
            constructionStack.Push(type);
            ConstructorInfo[] currentConstructors = type.GetConstructors();
            object createdStructure = default;
            if (currentConstructors.Length == 0)
            {
                createdStructure = Activator.CreateInstance(type);
                return createdStructure;
            }

            object[] constructorParams = null;
            ConstructorInfo chosenConstructor = null;
            bool isCreated = true;
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
                    isCreated = false;
                    continue;
                }
                if (isCreated)
                    break;
            }

            GenerateFieldsAndProperties(createdStructure,constructorParams,chosenConstructor);
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
                else if(param.ParameterType.IsGenericType)
                {
                    Type[] temp = param.ParameterType.GetGenericArguments();
                    newValue = generators[temp[0]].GetType().InvokeMember("GenerateList", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, generators[temp[0]], null);
                }
                else if(!(param.ParameterType.IsPrimitive || param.ParameterType == typeof(DateTime) || param.ParameterType == typeof(string)))
                {
                    newValue = this.GetType().GetMethod("Create").MakeGenericMethod(param.ParameterType).Invoke(this, null);
                }
                generatedParams.Add(newValue);
            }

            return generatedParams.ToArray();
        }

        private void GenerateFieldsAndProperties(object createdObject, object[] ctorParams, ConstructorInfo cInfo)
        {
            ParameterInfo[] pInfo = cInfo?.GetParameters();
            var fields = createdObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).Cast<MemberInfo>();
            var properties = createdObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Cast<MemberInfo>();
            var fieldsAndProperties = fields.Concat(properties);

            foreach (MemberInfo m in fieldsAndProperties)
            {
                bool wasInitialized = false;

                Type memberType = (m as FieldInfo)?.FieldType ?? (m as PropertyInfo)?.PropertyType;
                object memberValue = (m as FieldInfo)?.GetValue(createdObject) ?? (m as PropertyInfo)?.GetValue(createdObject);

                for (int i = 0; i < ctorParams?.Length; i++)
                {
                    object defaultValue = this.GetType().GetMethod("GetDefaultValue", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(memberType).Invoke(this, null);
                    if ((pInfo != null && ctorParams[i] == memberValue && memberType == pInfo[i].ParameterType && m.Name == pInfo[i].Name) || defaultValue?.Equals(memberValue) == false)
                    {
                        wasInitialized = true;
                        break;
                    }
                }
                if (!wasInitialized)
                {
                    object newValue = default;

                    if (memberType.IsPrimitive || memberType == typeof(DateTime))
                    {
                        newValue = generators[memberType].GetType().InvokeMember("Generate", BindingFlags.InvokeMethod | BindingFlags.Instance
                                                                                | BindingFlags.Public, null, generators[memberType], null);
                    }
                    else if (!(memberType.IsGenericType))
                    {
                        newValue = this.GetType().GetMethod("Create").MakeGenericMethod(memberType).Invoke(this, null);
                    }
                    else if (memberType.IsGenericType)
                    {
                        Type[] tmp = memberType.GetGenericArguments();
                        newValue = generators[tmp[0]].GetType().InvokeMember("GenerateList", BindingFlags.InvokeMethod | BindingFlags.Instance
                                                                             | BindingFlags.Public, null, generators[tmp[0]], null);
                    }
                   


                    (m as FieldInfo)?.SetValue(createdObject, newValue);
                    if ((m as PropertyInfo)?.CanWrite == true)
                    {
                        (m as PropertyInfo).SetValue(createdObject, newValue);
                    }
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

        private object GetDefaultValue<T>()
        {
            return default(T);
        }
    }
}
