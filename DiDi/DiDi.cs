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
        public T Get<T>() => (T)Get(typeof(T));
        public object Get(Type sT)
        {
            var specifier = dependencies.SingleOrDefault(x => x.ST == sT);
            if (specifier == null)
            {
                throw new Exception("not found");
            }
            if (specifier.Imple != null)
            {
                return specifier.Imple;
            }
            var relevantType = specifier.IT;
            var constInfo = relevantType.GetConstructors().First();
            if (constInfo.GetParameters().Any(x => chekCycle(sT, x.ParameterType)))
            {
                throw new Exception("cycle");
            }
            var parameters = constInfo.GetParameters().Select(x => Get(x.ParameterType)).ToArray();
            var implementation = Activator.CreateInstance(relevantType, parameters);
            if (specifier.LT == SLt.Singleton)
            {
                specifier.Imple = implementation;
            }
            return implementation;
        }
        public bool chekCycle(Type serviceType, Type parametrType)
        {
            var descriptor = dependencies.SingleOrDefault(x => x.ST == parametrType);
            var actualType = descriptor.IT;
            var constructorType = actualType.GetConstructors().First();
            return constructorType.GetParameters().Any(x => Equals(serviceType, x.ParameterType));
        }
    }
}