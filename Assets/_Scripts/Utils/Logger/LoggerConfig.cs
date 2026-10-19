using UnityEngine;

//[CreateAssetMenu] is an attribute that adds a custom menu item to Unity's "Create"
[CreateAssetMenu(fileName = "LoggerConfig", menuName = "Config/Logger Config")]
public class LoggerConfig : ScriptableObject
{
    [Header("General Settings")]
    public bool enableWarnings = true;
    public bool enableErrors = true;

    [Header("Log Categories")]
    public LogCategoryFlags enabledCategories = LogCategoryFlags.All;

    [System.Flags]
    public enum LogCategoryFlags
    {
        None = 0,               // 0
        Player = 1 << 0,        // 1
        Enemy = 1 << 1,         // 4
        Animation = 1 << 2,     // 8
        UI = 1 << 3,            // 16
        System = 1 << 4,        // 32
        All = ~0                
    }
}