using UnityEngine;

public class PlayerIdleState : PlayerMovementBaseState
{
    public override void EnterState(PlayerMovementStateManager mgr){
        //Debug.Log("player entered Idle state");
        mgr.Animator.SetBool(AnimatorParams.IsMoving, false);
    }

    public override void  UpdateState_Frame(PlayerMovementStateManager mgr){
        if(mgr.PlayerActions.MoveInput != Vector2.zero)
            mgr.ChangeState(mgr.RunningState);

    }

    public override void  UpdateState_Physics(PlayerMovementStateManager mgr){

    }
    public override void ExitState(PlayerMovementStateManager mgr){
        mgr.Animator.SetBool(AnimatorParams.IsMoving, true);
    }


}
