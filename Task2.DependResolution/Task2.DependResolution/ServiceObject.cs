using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.DependResolution
{
    public class ServiceObject
    {
        public Type typeService { get; set; }
        public Type typeImplimentation { get; set; }
        public object implimentation { get; set; }
        public bool singleTime { get; set; }
        
        public ServiceObject(Type service, Type typeImplimentation, bool singleTime)
        {
            typeService = service;
            this.typeImplimentation = typeImplimentation;
            this.singleTime = singleTime;
        }
    }
}
