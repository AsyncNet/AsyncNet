namespace AsyncNet.Selenium.Common.Configuration
{
    public class StaticSeleniumProvider : ISeleniumSettingsProvider
    {
        public ISeleniumSettings GetSettings()
        {
            return new SeleniumSettings
            {
                BaseUrl = "http://localhost:88"
            };
        }
    }
}
