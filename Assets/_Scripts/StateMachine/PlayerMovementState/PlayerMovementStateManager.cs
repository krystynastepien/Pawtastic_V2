using UnityEngine;

public class PlayerMovementStateManager : MonoBehaviour
{

    public PlayerMovementBaseState CurrentState {get; private set;}
    public PlayerActions PlayerActions {get; private set;}
    public Animator Animator {get; private set;}
    public PlayerRunningState RunningState {get; private set;}
    public PlayerIdleState IdleState {get; private set;}


    public void OnStart(){
        //Player = GameObject.FindWithTag(GameManager.Instance.Tag_player); 
        PlayerActions = this.gameObject.GetComponent<PlayerActions>();
        Animator = GetComponent<Animator>();
        RunningState = new PlayerRunningState();
        IdleState = new PlayerIdleState();

        CurrentState = IdleState;     
        CurrentState.EnterState(this); 
    }


    public void OnFrameUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState_Frame(this);
        }
    }

    public void OnUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState_Physics(this);
        }            
    }

    public void ChangeState(PlayerMovementBaseState newState){
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);
            CurrentState = newState;
            CurrentState.EnterState(this);
        }
    }
}
