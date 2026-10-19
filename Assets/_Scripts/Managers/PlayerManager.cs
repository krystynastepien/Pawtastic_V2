using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>, IManager
{
    #region Fields
    private GameObject _player;
    private PlayerActions _playerActions;
    private PlayerMovementStateManager _mngrMovementState;
    public PlayerHealthController _playerHealth;
    #endregion

    #region IManager Lifecycle Methods (GameFlow)
    public void OnAwake()
    {
        base.Initialize(); // Call the singleton logic
    }

    public void OnStart()
    {
        CreatePlayer();
        _playerActions.OnStart();
    }

    public void OnFrameUpdate()
    {
        //_playerActions.OnFrameUpdate();
        _mngrMovementState.OnFrameUpdate();
    }

    public void OnFixedUpdate()
    {
        _playerActions.OnFixedUpdate();
    }
    #endregion

    #region Initialization
    private void CreatePlayer()
    {
        InitPlayer();
        InitPlayerComponents();
    }

    /// <summary>
    /// Find Player Object
    /// </summary>
    private void InitPlayer()
    {
        _player = GameObject.FindWithTag(Tags.Player); 
        if(_player == null)
        {
            string msg = "Object with player tag: '" + Tags.Player + "' not found";
            GameLogger.LogError(msg, LoggerConfig.LogCategoryFlags.Player, _player);
        }
    }

    /// <summary>
    /// Initialize all player components
    /// </summary>
    private void InitPlayerComponents()
    {       
        if(_player == null)
        {
            string msg = "Object with player tag: '" + Tags.Player + "' not found";
            GameLogger.LogError(msg, LoggerConfig.LogCategoryFlags.Player, _player);
            return;
        }

        // get components
        _playerActions = _player.GetComponent<PlayerActions>();
        _mngrMovementState = _player.GetComponent<PlayerMovementStateManager>();
        _playerHealth = _player.GetComponent<PlayerHealthController>();

        _mngrMovementState.OnStart();
        _playerHealth.OnStart();
        //_playerStats = new PlayerStats(maxHealth: 10, attack: 5, defense: 2);        
    }
    #endregion
}