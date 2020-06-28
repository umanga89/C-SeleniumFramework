using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestAutomationFramework.Utilities
{
    class AppConfigManager
    {
      public static String GetBrowserConfigForKey(String key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
