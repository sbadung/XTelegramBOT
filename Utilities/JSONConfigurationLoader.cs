using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace XTelegramBOT.Utilities
{
    public class JSONConfigurationLoader : IConfigurationLoader
    {
        public IConfigurationRoot LoadConfiguration(string path)
        {
            bool notFound = !File.Exists(path);
            bool notJson = !Path.HasExtension(path) || !Path.GetExtension(path).Equals(".json");

            if (notFound) throw new FileNotFoundException("Unable to locate specified JSON file.", path);
            if (notJson) throw new ArgumentException("Only JSON files are supported.", nameof(path));

            try
            {
                return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path)
                    .Build();
            }
            catch (JsonException ex)
            {
                throw new JsonException("The specified JSON file is not valid.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Could not open file.", ex);
            }

        }
        public JSONConfigurationLoader() : base() { }
    }
}
