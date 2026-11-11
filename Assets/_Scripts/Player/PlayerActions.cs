
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(PlayerMovementStateManager))]
public class PlayerActions : MonoBehaviour
{
    // game const settings
    #region const 
    //private float playerSpeed;
    #endregion

    private PlayerMovementStateManager stateMovementMgr;
        

    private Rigidbody2D _rigidBody; 

    public Vector2 MoveInput { get; private set; }

    public void OnStart()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        //playerSpeed = GameManager.Instance.Player_BaseSpeed;
    }

    /// <summary>
    /// execute on Fixedupdate in GameFlow
    /// </summary>
    public void OnFixedUpdate()
    {
        OnMove();
    }

    #region Player InputAction collection
    //assigned in player input component 

    /// <summary>
    /// updates the _moveInput cariable accordingly to player's movement (WSAD) input
    /// </summary>
    /// <param name="context">collects input from the unity input system action callback</param>
    public void GetMoveInput(InputAction.CallbackContext context) 
    {
        // MoveInput = context.ReadValue<Vector2>();   brak normalizacji
        MoveInput = Vector2.ClampMagnitude(context.ReadValue<Vector2>(), 1f);   // normalizacja - ClampMagnitude(vector, float) - skróć długość vektora do podanej wartości
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Returns true if the movement engine detects move input different from 0
    /// </summary>
    /// <returns></returns>
    private void OnMove(){
        _rigidBody.linearVelocity = MoveInput * PlayerManager.Instance._playerHealth.Speed; 
    }
        
    #endregion
}
