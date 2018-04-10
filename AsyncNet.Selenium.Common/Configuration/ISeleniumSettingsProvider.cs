using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncNet.Selenium.Common.Configuration
{
    public interface ISeleniumSettingsProvider
    {
        ISeleniumSettings GetSettings();
    }
}
