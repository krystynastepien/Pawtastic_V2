using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour // T is generic type, ex. Singleton<GameManager>
{
    /// <summary>
    /// Singleton Instance
    /// </summary>
    public static T Instance { get; private set; }

    /// <summary>
    /// Initialize singleton instance
    /// </summary>
    protected virtual void Initialize()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this as T;
        GameLogger.Log("Created manager: " + Instance.GetType(), LoggerConfig.LogCategoryFlags.System, Instance);
    }
}