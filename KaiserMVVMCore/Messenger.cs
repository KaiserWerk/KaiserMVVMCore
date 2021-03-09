using KaiserMVVMCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KaiserMVVMCore
{
    public static class Messenger
    {
        private static Dictionary<Type, Action<object>> registrants = new Dictionary<Type, Action<object>>();
        private static Dictionary<Type, Func<object, Task>> registrantsAsync = new Dictionary<Type, Func<object, Task>>();
        private static readonly object registerLock = new object();
        private static readonly object sendLock = new object();

        public static void Send<T>(T obj) where T : class
        {
            lock (sendLock)
            {
                var set = registrants.Where(e => e.Key == typeof(T));
                foreach (var item in set)
                {
                    item.Value?.Invoke(obj);
                }

                var setAsync = registrantsAsync.Where(e => e.Key == typeof(T));
                foreach (var item in setAsync)
                {
                    item.Value?.Invoke(obj);
                }
            }
        }

        public static void Register<T>(Action<object> act, [CallerMemberName] string caller = "") where T : class
        {
            lock (registerLock)
            {
                if (!registrants.TryAdd(typeof(T), act))
                    throw new RecipientAlreadyRegisteredException($"This recipient class '{typeof(T)}' is already registered and cannot be registered again by '{caller}'.");
            }
        }

        public static void Register<T>(Func<object, Task> act, [CallerMemberName] string caller = "") where T : class
        {
            lock (registerLock)
            {
                if (!registrantsAsync.TryAdd(typeof(T), act))
                    throw new RecipientAlreadyRegisteredException($"This recipient class '{typeof(T)}' is already registered and cannot be registered again by '{caller}'.");
            }
        }
    }
}
