using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>, IManager
{
    #region Serialized Fields
    // [SerializeField] private string _isMovingParam = "isMoving_AnimatorParam";
    #endregion

    #region Properties
    #endregion 

    #region IManager Lifecycle Methods (GameFlow)
    /// <summary>
    /// create instance on Awake
    /// </summary>
    public void OnAwake()
    {
        base.Initialize();
    }

    public void OnStart()
    {
    }

    public void OnFrameUpdate()
    {
    }

    public void OnFixedUpdate()
    {
    }
    #endregion
}