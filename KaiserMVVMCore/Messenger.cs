using KaiserMVVMCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KaiserMVVMCore
{
    public static class Messenger
    {
        private static Dictionary<Type, Action<object>> registrants = new Dictionary<Type, Action<object>>();
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
            }
        }

        public static void Register<T>(Action<object> act) where T : class
        {
            lock (registerLock)
            {
                if (!registrants.TryAdd(typeof(T), act))
                    throw new RecipientAlreadyRegisteredException();
            }
        }
    }
}
