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

    public void RemoveInputs(InputSystem_Actions.Player_NISActions map,
        InputSystem_Actions.IPlayer_NISActions actions)
    {
        map.RemoveCallbacks(actions);
    }
    //public void RemoveInputs(InputSystem_Actions.Player_NISActions map,
    //InputSystem_Actions.IPlayer_NISActions actions)
    //{
    //    map.RemoveCallbacks(actions);
    //}
    //public void RemoveInputs(InputSystem_Actions.Player_NISActions map,
    //InputSystem_Actions.IPlayer_NISActions actions)
    //{
    //    map.RemoveCallbacks(actions);
    //}
    public void RemoveInputs(InputSystem_Actions.Player_Advc2DActions map,
    InputSystem_Actions.IPlayer_Advc2DActions actions)
    {
        map.RemoveCallbacks(actions);
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
        _inputs.Player_NIS.Move.performed += context => gameManager.PlayerController.OnMoveInput(context.ReadValue<Vector2>());
        _inputs.Player_NIS.Move.canceled += context => gameManager.PlayerController.OnMoveInput(Vector2.zero);

        _inputs.Player_NIS.Jump.started += _ => gameManager.PlayerController.OnJumpInput();

        _inputs.Player_NIS.Look.performed += context => gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        _inputs.Player_NIS.Look.canceled += context => gameManager.PlayerController.OnRotateInput(Vector2.zero);

        _inputs.Player_NIS.Zoom.performed += context => gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());

        _inputs.Player_NIS.CarSummon.started += _ => gameManager.PlayerController.OnCarSummonInput();
    }

    public void DOTween_InitDoomPlayerControll(GM_Doom gameManager)
    {
        _inputs.Player_DOT.Move.started += context => gameManager.Player.OnWalkDirectionChoose(context.ReadValue<float>(), ref gameManager.Player.WalkDirection);
        _inputs.Player_DOT.Move.performed += context => gameManager.Player.OnMoveInput(context);

        _inputs.Player_DOT.Move.canceled += context => gameManager.Player.OnActionStop(Actions.Move);
        _inputs.Player_DOT.Move.canceled += _ => gameManager.Player.OnWalkDirectionChoose(0, ref gameManager.Player.WalkDirection);

        _inputs.Player_DOT.Rotate.started += context => gameManager.Player.OnWalkDirectionChoose(context.ReadValue<float>(), ref gameManager.Player.RotateDirection);
        _inputs.Player_DOT.Rotate.performed += context => gameManager.Player.OnRotateInput(context);
        _inputs.Player_DOT.Rotate.canceled += _ => gameManager.Player.OnActionStop(Actions.Rotate);
        _inputs.Player_DOT.Rotate.canceled += _ => gameManager.Player.OnWalkDirectionChoose(0, ref gameManager.Player.RotateDirection);

        _inputs.Player_DOT.Jump.performed += context => gameManager.Player.OnJumpInput(context);
        _inputs.Player_DOT.Jump.canceled += _ => gameManager.Player.OnActionStop(Actions.Jump);

        _inputs.Player_DOT.Pause.started += _ => gameManager.TogglePauseMinigame();

        _inputs.Player_DOT.Move.started += _ => gameManager.Player.OnActionChoose(Actions.Move);
        _inputs.Player_DOT.Rotate.started += _ => gameManager.Player.OnActionChoose(Actions.Rotate);
        _inputs.Player_DOT.Jump.started += _ => gameManager.Player.OnActionChoose(Actions.Jump);

        _inputs.Player_DOT.Move.started += _ => gameManager.Player.OnActionChoose(Actions.Move);
        _inputs.Player_DOT.Rotate.started += _ => gameManager.Player.OnActionChoose(Actions.Rotate);
        _inputs.Player_DOT.Jump.started += _ => gameManager.Player.OnActionChoose(Actions.Jump);
    }

    public void TileMap_InitPlayerController(GM_Tilemap gameManager)
    {
        _inputs.Player_Tilemap.Move.started += context => gameManager.Player.OnMoveInput(context.ReadValue<Vector2>());
        _inputs.Player_Tilemap.Move.performed += context => gameManager.Player.OnMoveInput(context.ReadValue<Vector2>());
        _inputs.Player_Tilemap.Move.canceled += _ => gameManager.Player.OnMoveInput(Vector2.zero);

        _inputs.Player_Tilemap.Jump.started += context => gameManager.Player.OnJumpInput();
    }

    public void Advc2D_InitPlayerController(GM_Advanced2D gameManager)
    {
        print("init controller");
    }
}