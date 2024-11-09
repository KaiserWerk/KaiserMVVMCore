using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KaiserMVVMCore;

public class Container
{
    public static Container Default { get; } = new Container();

    private Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>();
    private Dictionary<Type, object> instances = new Dictionary<Type, object>();

    public void Register<TInterface, TClass>()
        where TInterface : class
        where TClass : class, TInterface
    {
        Type type1 = typeof(TInterface);
        Type type2 = typeof(TClass);
        if (!this.registeredTypes.TryAdd(type1, type2))
        {
            throw new InvalidOperationException($"Type {type1.FullName} already registered");
        }
    }

    public void Register<TClass>() where TClass : class
    {
        Type type = typeof(TClass);
        if (!this.registeredTypes.TryAdd(type, type))
        {
            throw new InvalidOperationException($"Type {type.FullName} already registered");
        }
    }

    public TService GetInstance<TService>()
    {
        Type type = typeof(TService);
        return (TService)this.DoGetService(type);
    }


    private ConstructorInfo GetConstructorInfo(Type serviceType)
    {
        Type type1;
        if (this.registeredTypes.ContainsKey(serviceType))
        {
            Type type2 = this.registeredTypes[serviceType];
            if ((object)type2 == null)
                type2 = serviceType;
            type1 = type2;
        }
        else
            type1 = serviceType;
        ConstructorInfo[] array = type1.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic).ToArray();
        if (array.Length == 0)
            throw new ArgumentException($"Cannot register: No public constructor found in {serviceType.FullName}.");
        return array[0];
    }

    private object MakeInstance(Type type)
    {
        ConstructorInfo constructorInfo = this.GetConstructorInfo(type);
        ParameterInfo[] parameters1 = constructorInfo.GetParameters();
        if (parameters1.Length == 0)
            return constructorInfo.Invoke(new object[0]);
        object[] parameters2 = new object[parameters1.Length];
        foreach (ParameterInfo parameterInfo in parameters1)
            parameters2[parameterInfo.Position] = this.DoGetService(parameterInfo.ParameterType);
        return constructorInfo.Invoke(parameters2);
    }

    private object DoGetService(Type type)
    {
        if (this.instances.TryGetValue(type, out object instance))
        {
            return instance;
        }

        if (!this.registeredTypes.TryGetValue(type, out Type derivedType))
        {
            throw new Exception($"Derived Type {type.FullName} not found");
        }

        object newInstance = this.MakeInstance(derivedType);
        this.instances.Add(type, newInstance);
        return newInstance;
    }
}


