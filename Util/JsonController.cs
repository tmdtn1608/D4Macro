using System.IO;
using Newtonsoft.Json;

namespace D4Macro.Util;

public class JsonController
{
    private static readonly Lazy<JsonController> lazy =
        new Lazy<JsonController>(() => new JsonController());

    public static JsonController Instance { get { return lazy.Value; } }

    private JsonController()
    {
    }

    public T ReadJson<T>(string path)
    {
        if (path == null || path == string.Empty) throw new ArgumentNullException("need path");
        var readJson = File.ReadAllText(path);
        T result = JsonConvert.DeserializeObject<T>(readJson);
        return result;
    } 
    
    public void WriteJson(object value, string path)
    {
        if (value == null || path == null || path == string.Empty) throw new ArgumentNullException("need object and path");
        var config = JsonConvert.SerializeObject(value, Formatting.Indented);
        File.WriteAllText(path, config);
    }

    public void DeleteJson(string path)
    {
        File.Delete(path);
    }
}