using NLog;
using RazorProjectV5.Services.Interfaces;
using System.Text.Json;

namespace RazorProjectV5.Services.ServiceClasses
{
    public class JsonFileService<T> : IJsonFileService<T>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string JsonFileName;
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger(); // NLog Logger instance

        public JsonFileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            JsonFileName = Path.Combine(_webHostEnvironment.WebRootPath, "Data", typeof(T).Name + "s.json");
        }

        public async Task SaveAsync(IEnumerable<T> objects)
        {
            try
            {
                using (FileStream jsonFileWriter = File.Create(JsonFileName))
                {
                    await JsonSerializer.SerializeAsync(jsonFileWriter, objects, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Failed to save JSON objects.");
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                using (StreamReader jsonFileReader = File.OpenText(JsonFileName))
                {
                    var json = await jsonFileReader.ReadToEndAsync();
                    return JsonSerializer.Deserialize<IEnumerable<T>>(json);
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Failed to retrieve JSON objects.");
                throw;
            }
        }
    }
}
