using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private BirdController controller;

    private void OnEnable()
    {
        SetInitialReferences();

        inputActions.BirdNormal.Enable();

        inputActions.BirdNormal.Move.performed += Move_performed;
    }

    private void OnDisable()
    {
        inputActions.BirdNormal.Move.performed -= Move_performed;
        inputActions.BirdNormal.Disable();
    }

    private void SetInitialReferences()
    {
        inputActions = new PlayerInputActions();
        controller = GetComponent<BirdController>();
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        controller.MoveBird();
    }
}
