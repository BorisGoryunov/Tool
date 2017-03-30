using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ConfigManager.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParameterAttribute : Attribute
    {
        public string Name { get; private set; }

        public ParameterAttribute()
        {

        }

        public ParameterAttribute(string name)
        {
            Name = name;
        }
    }
}
