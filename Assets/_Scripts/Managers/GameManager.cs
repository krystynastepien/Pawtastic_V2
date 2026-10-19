using UnityEngine;

public class GameManager : Singleton<GameManager>, IManager
{
    #region Serialized Fields
    #endregion

    #region Properties

    #endregion

    #region IManager Lifecycle Methods (GameFlow)
    public void OnAwake()
    {
        base.Initialize(); // Call the singleton logic
    }

    public void OnStart()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnFrameUpdate()
    {
    }
    #endregion

    #region Public Methods
    // Your public methods here
    #endregion

    #region Private Methods
    // Your private helper methods here
    #endregion
}