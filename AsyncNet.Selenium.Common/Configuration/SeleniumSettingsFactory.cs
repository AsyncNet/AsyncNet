namespace AsyncNet.Selenium.Common.Configuration
{
    public class SeleniumSettingsFactory
    {
        public enum ProviderSource
        {
            AppConfig,
            JSON,
            Hardcoded
        }

        public ISeleniumSettingsProvider GetSettingsProvider(ProviderSource source)
        {
            switch(source)
            {
                case ProviderSource.AppConfig:
                case ProviderSource.JSON:
                default:
                    return new StaticSeleniumProvider();
            }
        }
    }
}
