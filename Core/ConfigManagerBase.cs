using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class ConfigManagerBase
    {
        protected System.Configuration.Configuration Config { get; private set; }
        private string[] _keys;
        private readonly string _assemblyPath = Assembly.GetEntryAssembly().Location;

        /// <summary>
        /// Загружает все параметры
        /// </summary>
        public void Open()
        {
            OpenExeConfiguration();
            Load();
        }

        
        public IEnumerable<Common.ConnectionStringSetting> GetConnectionStrings()
        {
            var result = new List<Common.ConnectionStringSetting>();
            foreach(System.Configuration.ConnectionStringSettings item in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                var resultItem = new Common.ConnectionStringSetting();
                resultItem.Name = item.Name;
                resultItem.ConnectionString = item.ConnectionString;
                result.Add(resultItem);
            }
            return result
                    .Skip(1);
        }

        public string GetConnectionString(string connectionName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        protected virtual void Load()
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is ConfigManager.Schema.ParameterAttribute)
                    {
                        var param = attribute as ConfigManager.Schema.ParameterAttribute;
                        string paramName = null;
                        if (string.IsNullOrEmpty(param.Name))
                        {
                            paramName = property.Name;
                        }
                        else
                        {
                            paramName = param.Name;
                        }
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(this, GetValue(paramName));
                        }
                        if (property.PropertyType == typeof(int))
                        {
                            property.SetValue(this, GetValueAsInt(paramName));
                        }
                        if (property.PropertyType == typeof(bool))
                        {
                            property.SetValue(this, GetValueAsBool(paramName));
                        }
                    }
                }
            }
        }

        protected virtual void SetValues()
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is ConfigManager.Schema.ParameterAttribute)
                    {
                        var param = attribute as ConfigManager.Schema.ParameterAttribute;
                        string paramName = null;
                        if (string.IsNullOrEmpty(param.Name))
                        {
                            paramName = property.Name;
                        }
                        else
                        {
                            paramName = param.Name;
                        }
                        if (property.PropertyType == typeof(string))
                        {
                            SetValue(paramName, (string)property.GetValue(this));
                        }
                        if (property.PropertyType == typeof(int))
                        {
                            SetValue(paramName, (int)property.GetValue(this));
                        }
                        if (property.PropertyType == typeof(bool))
                        {
                            SetValue(paramName, (bool)property.GetValue(this));
                        }
                    }
                }
            }

        }

        private void OpenExeConfiguration()
        {
            Config = System.Configuration.ConfigurationManager.OpenExeConfiguration(_assemblyPath);
            _keys = Config.AppSettings.Settings.AllKeys;
        }

        private void SaveExeConfiguration()
        {
            Config.Save();
        }

        public void Save()
        {
            OpenExeConfiguration();
            SetValues();
            SaveExeConfiguration();
        }

        protected void Save(string name, int value)
        {
            OpenExeConfiguration();
            SetValue(name, value);
            SaveExeConfiguration();
        }

        protected void SetValue(string name, string value)
        {
            Config.AppSettings.Settings.Remove(name);
            Config.AppSettings.Settings.Add(name, value);
        }

        protected void SetValue(string name, int value)
        {
            SetValue(name, value.ToString());
        }

        protected void SetValue(string name, bool value)
        {
            SetValue(name, Convert.ToString(value));
        }


        protected string GetValue(string name)
        {
            if (Config == null)
            {
                OpenExeConfiguration();
            }
            if (_keys.Contains(name) == false)
            {
                throw new KeyNotFoundException($"Параметр '{name}' не найден в  {Config.FilePath}");
            }
            return Config.AppSettings.Settings[name].Value;
        }

        protected int GetValueAsInt(string name)
        {
            string value = GetValue(name);
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            else
            {
                int result;
                if (int.TryParse(value, out result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }

        protected bool GetValueAsBool(string name)
        {
            string result = GetValue(name);
            if (string.IsNullOrEmpty(result))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(result);
            }
        }


    }
}
