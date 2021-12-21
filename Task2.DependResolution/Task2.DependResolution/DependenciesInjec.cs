using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.DependResolution
{
    public class DependenciesInjec
    {
        private List<ServiceObject> _dependencies;
        private List<Type> types = new List<Type>();
        public DependenciesInjec()
        {
            _dependencies = new List<ServiceObject>();
        }
        public void AddTransient<TService, TImplementation>() where TImplementation : TService
        {
            _dependencies.Add(new ServiceObject(typeof(TService), typeof(TImplementation), false));
        }
        public void AddSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _dependencies.Add(new ServiceObject(typeof(TService), typeof(TImplementation), true));
        }
        public object Get(Type T)
        {
            ServiceObject service = null;

            foreach (var item in _dependencies)
            {
                if (item.typeService.Equals(T))
                {
                    service = item;
                }
                else
                {
                    throw new KeyNotFoundException("Type not found!");
                }
            }
            if (!types.Contains(service.typeService))
            {
                types.Add(service.typeService);
            }
            else
            {
                throw new CycleException("A cycle has arisen, it is impossible to resolve the dependency!");
            }
            if (service.implimentation != null)
            {
                types.Clear();
                return service.implimentation;
                
            }
            
            var actualtype = service.typeImplimentation ?? service.typeService;
            if (actualtype.IsAbstract || actualtype.IsInterface)
            {
                throw new ServiceException("Service did't have ctor");
            }
            var constructor = actualtype.GetConstructors().First();
            var parameters = constructor.GetParameters();
            List<object?> listParameters = new List<object?>();
            foreach (var item in parameters)
            {
                var tempType = Get(item.ParameterType);
                listParameters.Add(tempType);
            }
            if (service.singleTime)
            {
                if (service.implimentation == null)
                {
                    service.implimentation = Activator.CreateInstance(actualtype, listParameters.ToArray());
                    types.Clear();
                    return service.implimentation;
                }
                else
                {
                    types.Clear();
                    return service.implimentation;
                }
            }
            else
            {
                types.Clear();
                return Activator.CreateInstance(actualtype, listParameters.ToArray());
            }
        }
        public object Get<T>()
        {
            return Get(typeof(T));
        }
    }
}
