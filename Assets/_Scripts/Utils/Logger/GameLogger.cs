using System.Runtime.CompilerServices;
using UnityEngine;

public static class GameLogger
{
    private static LoggerConfig _config;

    private static LoggerConfig Config
    {
        get
        {
            if (_config == null)
            {
                _config = Resources.Load<LoggerConfig>("System_Prefabs/LoggerConfig");
                if (_config == null)
                {
                    Debug.LogWarning("LoggerConfig prefab not found in Resources folder.");
                }
            }
            return _config;
        }
    }

    #region Log Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="category"></param>
    /// <param name="context"></param>
    public static void Log(string message, LoggerConfig.LogCategoryFlags category, Object context = null, [CallerMemberName] string caller = null)
    {
        if (Config != null && (Config.enabledCategories & category) != 0)
        {
            Debug.Log($"[LOG][{caller}()] [{category}]:  {message}", context);
        }
    }

    public static void LogWarning(string message, LoggerConfig.LogCategoryFlags category, Object context = null, [CallerMemberName] string caller = null)
    {
        if (Config != null && Config.enableWarnings && (Config.enabledCategories & category) != 0)
        {
            Debug.LogWarning($"[WARNING][{caller}()] [{category}]:  {message}", context);
        }
    }

    public static void LogError(string message, LoggerConfig.LogCategoryFlags category, Object context = null, [CallerMemberName] string caller = null)
    {
        if (Config != null && Config.enableErrors && (Config.enabledCategories & category) != 0)
        {
            Debug.LogError($"[ERROR][{caller}()] [{category}]:  {message}", context);
        }
    }

    #endregion
}