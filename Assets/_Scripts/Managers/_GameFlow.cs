using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(AnimationManager))]
public class GameFlow : MonoBehaviour
{
    //public static GameFlow Instance { get; private set; }  // singleton - globally accesible class with only 1 instance   
    [SerializeField] private GameManager _gameMngr;
    [SerializeField] private PlayerManager _playerMngr;
    [SerializeField] private HUDManager _hudMngr;
    [SerializeField] private AnimationManager _animationMngr;

    private void Awake()
    {
        // get all manager objects
        GetManagerComponents();

        // initialize all other singletons    
        _gameMngr.OnAwake();
        _playerMngr.OnAwake();

        _hudMngr.OnAwake();
        _animationMngr.OnAwake();
    }

    void Start()
    {
        _playerMngr.OnStart();
        _hudMngr.OnStart();
    }

    void FixedUpdate()    // Update is called 50 times per second (not once per frame like Update) - for physics
    {
        _playerMngr.OnFixedUpdate();
    }

    void Update()    // Update is called once per frame - for animation
    {
        _playerMngr.OnFrameUpdate();
    }


    /// <summary>
    /// initialize all manager components in the GameFlow object
    /// </summary>
    private void GetManagerComponents(){
        _gameMngr = GetComponent<GameManager>();
        _playerMngr = GetComponent<PlayerManager>();
        _hudMngr = GetComponent<HUDManager>();

        _animationMngr = GetComponent<AnimationManager>();
    }
}