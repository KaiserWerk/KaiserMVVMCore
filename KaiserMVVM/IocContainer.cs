using System;
using System.Collections.Generic;

namespace KaiserMVVM
{
    [Flags]
    public enum RegisterMode
    {
        None,
        Singleton,
        SingleUse
    }

    public static class IocContainer
    {
        private static Dictionary<Type, (object, RegisterMode, object[])> instances = new Dictionary<Type, (object, RegisterMode, object[])>();
        private static object lockObj = new object();

        public static void Register<T>(RegisterMode mode = RegisterMode.None, params object[] parameters) where T : class
        {
            lock (lockObj)
            {
                bool added = instances.TryAdd(typeof(T), (null, mode, parameters));
                if (!added)
                    throw new Exception($"Could not register type '{typeof(T)}'; is it already registered?");
            }
        }

        public static T GetInstance<T>() where T : class
        {
            lock (lockObj)
            {
                bool exists = instances.TryGetValue(typeof(T), out (object, RegisterMode, object[]) instanceInfo);
                if (!exists)
                    throw new Exception($"No instance of type '{typeof(T)}' registered!");

                if (instanceInfo.Item1 != null)
                {
                    return (T)instanceInfo.Item1;
                }

                if ((instanceInfo.Item2 & RegisterMode.Singleton) != RegisterMode.None)
                {
                    var instance = Activator.CreateInstance(typeof(T), instanceInfo.Item3);
                    instances[typeof(T)] = (instance, instanceInfo.Item2, instanceInfo.Item3);
                    return (T)instance;
                }

                if ((instanceInfo.Item2 & RegisterMode.SingleUse) != RegisterMode.None)
                {
                    return (T)Activator.CreateInstance(typeof(T), instanceInfo.Item3);
                }

                // create proper exception type
                throw new Exception($"Could not obtain instance for type {nameof(T)}");
            }
        }
    }
}
