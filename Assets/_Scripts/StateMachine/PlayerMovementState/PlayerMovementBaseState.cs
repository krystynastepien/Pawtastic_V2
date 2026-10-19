using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class PlayerMovementBaseState
{
    public abstract void EnterState(PlayerMovementStateManager mgr);
    public abstract void UpdateState_Frame(PlayerMovementStateManager mgr);
    public abstract void UpdateState_Physics(PlayerMovementStateManager mgr);
    public abstract void ExitState(PlayerMovementStateManager mgr);

}

