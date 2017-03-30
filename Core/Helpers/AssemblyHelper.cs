using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class AssemblyHelper
    {
        public static string GetAssemblyPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        public static string GetAssemblyExePath()
        {
            string result = Assembly.GetCallingAssembly().Location;
            return result;
        }

        public static Assembly GetAssembly(Type type)
        {
            return Assembly.GetAssembly(type);
        }
    }
}
