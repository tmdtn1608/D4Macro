using System.IO;

namespace D4Macro.Util;

public static class Const
{
    public static readonly string SETTING_FILE_PATH =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "D4Macro", "config.json");
    public static readonly string DATA_FILE_PATH = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "D4Macro","quickSave.json");
    public const string PROCESS_NAME = "Diablo IV";
    // public const string PROCESS_NAME = "notepad";
    
    public enum LaunchKeyEnum
    {
        F1 = 90,
        F2 = 91,
        F3 = 92,
        F4 = 93,
        F5 = 94,
        F6 = 95,
        F7 = 96,
        F8 = 97,
        F9 = 98,
        F10 = 99,
        F11 = 100,
        F12 = 101,
        Delete = 32,
        Home = 22,
        End = 21,
        PageUp = 19,
        PageDown = 20
    }
}