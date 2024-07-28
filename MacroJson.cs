using System.IO;
using Newtonsoft.Json;

namespace D4Macro;

public class MacroJson
{
    public int Key1Interval { get; set; }
    public bool Key1Enabled { get; set; }
    public int Key2Interval { get; set; }
    public bool Key2Enabled { get; set; }
    public int Key3Interval { get; set; }
    public bool Key3Enabled { get; set; }
    public int Key4Interval { get; set; }
    public bool Key4Enabled { get; set; }
    public int MouseLeftInterval { get; set; }
    public bool MouseLeftEnabled { get; set; }
    public int MouseRightInterval { get; set; }
    public bool MouseRightEnabled { get; set; }

    public static void SaveSettings(MacroJson settings, string filePath)
    {
        var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static MacroJson LoadSettings(string filePath)
    {
        if (!File.Exists(filePath))
            return new MacroJson();

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<MacroJson>(json);
    }
}