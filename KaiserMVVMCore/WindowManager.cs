using System;
using System.Collections.Generic;

namespace KaiserMVVMCore
{
    public static class WindowManager
    {
        private static Dictionary<Type, IWindow> windowDict = new Dictionary<Type, IWindow>();

        public static void Open<T>() where T : IWindow
        {
            bool exists = windowDict.TryGetValue(typeof(T), out IWindow win);
            if (exists)
            {
                var t = (T)win;
                t.Close();
            }

            IWindow w = (T)Activator.CreateInstance(typeof(T));
            windowDict[typeof(T)] = w;

            w?.Show();
        }

        public static void Close<T>() where T : IWindow
        {
            bool exists = windowDict.TryGetValue(typeof(T), out IWindow win);
            if (exists)
            {
                windowDict.Remove(typeof(T));
                win?.Close();
            }
        }

        public static IWindow GetInstance<T>() where T : IWindow
        {
            bool exists = windowDict.TryGetValue(typeof(T), out IWindow win);
            if (exists)
            {
                return (T)win;
            }

            IWindow w = (T)Activator.CreateInstance(typeof(T));
            windowDict[typeof(T)] = w;

            return w;
        }
    }
}
