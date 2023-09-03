using Microsoft.Extensions.Configuration;

namespace XTelegramBOT.Utilities
{
    public interface IConfigurationLoader
    {
        IConfigurationRoot LoadConfiguration(string path);
    }
}
