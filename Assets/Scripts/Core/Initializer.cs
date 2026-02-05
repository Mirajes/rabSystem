using UnityEngine;

public class Initializer : MonoBehaviour
{
    public static Initializer Instance { get; private set; }
    public static InputSystem_Actions _inputs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _inputs = new();
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.Dispose();
    }

    public void EnableInputs()
    {
        _inputs.Enable();
    }

    public void NIS_InitCarControll(GM_NewInputSystem gameManager)
    {
        //Transport

        _inputs.Transport.Zoom.performed += context => gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());

        _inputs.Transport.Look.performed += context => gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        _inputs.Transport.Look.canceled += _ => gameManager.PlayerController.OnRotateInput(Vector2.zero);

        _inputs.Transport.Move.performed += context => gameManager.CurrentTransport.OnControllerInput(context.ReadValue<Vector2>());
        _inputs.Transport.Move.canceled += _ => gameManager.CurrentTransport.OnControllerInput(Vector2.zero);

        _inputs.Transport.CarExit.started += _ => gameManager.ChangeMap(false);
    }
    public void NIS_InitDefaultPlayerControll(GM_NewInputSystem gameManager)
    {
        // Player
        _inputs.Player.Move.performed += context => gameManager.PlayerController.OnMoveInput(context.ReadValue<Vector2>());
        _inputs.Player.Move.canceled += context => gameManager.PlayerController.OnMoveInput(Vector2.zero);

        _inputs.Player.Jump.started += _ => gameManager.PlayerController.OnJumpInput();

        _inputs.Player.Look.performed += context => gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        _inputs.Player.Look.canceled += context => gameManager.PlayerController.OnRotateInput(Vector2.zero);

        _inputs.Player.Zoom.performed += context => gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());

        _inputs.Player.CarSummon.started += _ => gameManager.PlayerController.OnCarSummonInput();
    }



    public void DOTween_InitDoomPlayerControll()
    {
        //_inputs.CameraTest.Look.performed += callback => 
    }
}