using System;
using System.Collections.Generic;
using System.Linq;

namespace DiDi
{
    public class DiDi
    {
        private List<SDe> dependencies;
        public DiDi()
        {
            dependencies = new List<SDe>();
        }
        public void AddUnstable<TService, TImplementation>()
        {
            dependencies.Add(new SDe(typeof(TService), typeof(TImplementation), SLt.Transient));
        }
        public void AddSingleton<TService, TImplementation>()
        {
            dependencies.Add(new SDe(typeof(TService), typeof(TImplementation), SLt.Singleton));
        }
        //public T Get<T>() => (T)Get(typeof(T));
        public object Get(Type serviceType)
                {
                    List<Type> mylistik = new List<Type>();
        
                    return Get(serviceType, mylistik);
                }
        public object Get(Type serviceType, List<Type> parlist)
                {
                    var descriptor = dependencies.SingleOrDefault(x => x.ST == serviceType);
                    if (descriptor == null)
                    {
                        throw new Exception("нет никого");
                    }
                    if (descriptor.Imple != null)
                    {
                        return descriptor.Imple;
                    }
                    var actualType = descriptor.IT;
                    var constructor = actualType.GetConstructors().First();
                    List<object> chekaem = new List<object>();
        
                    foreach (var parameter in constructor.GetParameters())
                    {
                        if (parlist.Contains(serviceType))
                        {
                            throw new Exception("цикл");
                        }
                        parlist.Add(serviceType);
        
                        var newParameter = Get(parameter.ParameterType, parlist);
        
                        parlist.Remove(serviceType);
        
                        chekaem.Add(newParameter);
                    }
                    
                    var parameters = chekaem.ToArray();
                    var implementation = Activator.CreateInstance(actualType, parameters);
                    if (descriptor.LT == SLt.Singleton)
                    {
                        descriptor.Imple = implementation;
                    }
                    return implementation;
                }
        
    }
}