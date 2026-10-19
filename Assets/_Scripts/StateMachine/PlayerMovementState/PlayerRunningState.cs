using UnityEngine;

public class PlayerRunningState : PlayerMovementBaseState
{
    public override void EnterState(PlayerMovementStateManager mgr){
        //Debug.Log("player entered Running state");
        mgr.Animator.SetBool(AnimatorParams.IsMoving, true);
    }

    public override void  UpdateState_Frame(PlayerMovementStateManager mgr){
        if( mgr.PlayerActions.MoveInput != Vector2.zero){
            mgr.Animator.SetFloat(AnimatorParams.HorizontalVelocity, mgr.PlayerActions.MoveInput.x);
            mgr.Animator.SetFloat(AnimatorParams.VerticalVelocity, mgr.PlayerActions.MoveInput.y);
        }
        else{
            mgr.ChangeState(mgr.IdleState);
        }
    }

    public override void  UpdateState_Physics(PlayerMovementStateManager mgr){
    }

    public override void ExitState(PlayerMovementStateManager mgr){
        mgr.Animator.SetBool(AnimatorParams.IsMoving, false);
    }

}
