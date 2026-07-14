using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayerHandler : MonoBehaviour
{
    public static InputPlayerHandler Instance;

    public event Action<int> OnInterationPress;

    private PlayerActionsMap inputActions;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        inputActions = new PlayerActionsMap();
        inputActions.Enable();
    }

    private void Start()
    {
        inputActions.Player.Interaction_Main.performed += Interaction_Main_performed;
        inputActions.Player.Interaction_Alt.performed += Interaction_Alt_performed;
        
    }
    private void OnDisable()
    {
        inputActions.Player.Interaction_Main.performed -= Interaction_Main_performed;
        inputActions.Player.Interaction_Alt.performed -= Interaction_Alt_performed;
        inputActions.Disable();
    }

    public Vector2 GetMovimentInputNormalized() => inputActions.Player.Moviment.ReadValue<Vector2>();

    private void Interaction_Main_performed(InputAction.CallbackContext e)
    {
        OnInterationPress?.Invoke(1);
    }
     private void Interaction_Alt_performed(InputAction.CallbackContext e)
    {
        OnInterationPress?.Invoke(2);
    }

}
