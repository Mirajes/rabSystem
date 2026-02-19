using DG.Tweening.Core.Easing;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public static Initializer Instance { get; private set; }
    public static InputSystem_Actions Inputs;

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

        Inputs = new();
    }

    private void OnDisable()
    {
        Inputs.Disable();
        Inputs.Dispose();
    }

    public void EnableInputs()
    {
        Inputs.Enable();
    }

    #region InitInputs
    public void NIS_InitCarControll(GM_NewInputSystem gameManager)
    {
        //Transport

        Inputs.Transport.Zoom.performed += context => gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());

        Inputs.Transport.Look.performed += context => gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        Inputs.Transport.Look.canceled += _ => gameManager.PlayerController.OnRotateInput(Vector2.zero);

        Inputs.Transport.Move.performed += context => gameManager.CurrentTransport.OnControllerInput(context.ReadValue<Vector2>());
        Inputs.Transport.Move.canceled += _ => gameManager.CurrentTransport.OnControllerInput(Vector2.zero);

        Inputs.Transport.CarExit.started += _ => gameManager.ChangeMap(false);
    }
    public void NIS_InitDefaultPlayerControll(GM_NewInputSystem gameManager)
    {
        // Player
        Inputs.Player_NIS.Move.performed += context => gameManager.PlayerController.OnMoveInput(context.ReadValue<Vector2>());
        Inputs.Player_NIS.Move.canceled += context => gameManager.PlayerController.OnMoveInput(Vector2.zero);

        Inputs.Player_NIS.Jump.started += _ => gameManager.PlayerController.OnJumpInput();

        Inputs.Player_NIS.Look.performed += context => gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        Inputs.Player_NIS.Look.canceled += context => gameManager.PlayerController.OnRotateInput(Vector2.zero);

        Inputs.Player_NIS.Zoom.performed += context => gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());

        Inputs.Player_NIS.CarSummon.started += _ => gameManager.PlayerController.OnCarSummonInput();
    }

    public void DOTween_InitDoomPlayerControll(GM_Doom gameManager)
    {
        Inputs.Player_DOT.Move.started += context => gameManager.Player.OnWalkDirectionChoose(context.ReadValue<float>(), ref gameManager.Player.WalkDirection);
        Inputs.Player_DOT.Move.performed += context => gameManager.Player.OnMoveInput(context);

        Inputs.Player_DOT.Move.canceled += context => gameManager.Player.OnActionStop(Actions.Move);
        Inputs.Player_DOT.Move.canceled += _ => gameManager.Player.OnWalkDirectionChoose(0, ref gameManager.Player.WalkDirection);

        Inputs.Player_DOT.Rotate.started += context => gameManager.Player.OnWalkDirectionChoose(context.ReadValue<float>(), ref gameManager.Player.RotateDirection);
        Inputs.Player_DOT.Rotate.performed += context => gameManager.Player.OnRotateInput(context);
        Inputs.Player_DOT.Rotate.canceled += _ => gameManager.Player.OnActionStop(Actions.Rotate);
        Inputs.Player_DOT.Rotate.canceled += _ => gameManager.Player.OnWalkDirectionChoose(0, ref gameManager.Player.RotateDirection);

        Inputs.Player_DOT.Jump.performed += context => gameManager.Player.OnJumpInput(context);
        Inputs.Player_DOT.Jump.canceled += _ => gameManager.Player.OnActionStop(Actions.Jump);

        Inputs.Player_DOT.Pause.started += _ => gameManager.TogglePauseMinigame();

        Inputs.Player_DOT.Move.started += _ => gameManager.Player.OnActionChoose(Actions.Move);
        Inputs.Player_DOT.Rotate.started += _ => gameManager.Player.OnActionChoose(Actions.Rotate);
        Inputs.Player_DOT.Jump.started += _ => gameManager.Player.OnActionChoose(Actions.Jump);

        Inputs.Player_DOT.Move.started += _ => gameManager.Player.OnActionChoose(Actions.Move);
        Inputs.Player_DOT.Rotate.started += _ => gameManager.Player.OnActionChoose(Actions.Rotate);
        Inputs.Player_DOT.Jump.started += _ => gameManager.Player.OnActionChoose(Actions.Jump);
    }

    public void TileMap_InitPlayerController(GM_Tilemap gameManager)
    {
        Inputs.Player_Tilemap.Move.started += context => gameManager.Player.OnMoveInput(context.ReadValue<Vector2>());
        Inputs.Player_Tilemap.Move.performed += context => gameManager.Player.OnMoveInput(context.ReadValue<Vector2>());
        Inputs.Player_Tilemap.Move.canceled += _ => gameManager.Player.OnMoveInput(Vector2.zero);

        Inputs.Player_Tilemap.Jump.started += context => gameManager.Player.OnJumpInput();
    }

    public void Advc2D_InitPlayerController(GM_Advanced2D gameManager)
    {
        print("init controller");
    }
    #endregion

    #region RemoveInputs
    public void RemoveInputs(GM_NewInputSystem gameManager)
    {
        Inputs.Transport.Zoom.performed -= context => gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());
        Inputs.Transport.Look.performed -= context => gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        Inputs.Transport.Look.canceled -= _ => gameManager.PlayerController.OnRotateInput(Vector2.zero);
        Inputs.Transport.Move.performed -= context => gameManager.CurrentTransport.OnControllerInput(context.ReadValue<Vector2>());
        Inputs.Transport.Move.canceled -= _ => gameManager.CurrentTransport.OnControllerInput(Vector2.zero);
        Inputs.Transport.CarExit.started -= _ => gameManager.ChangeMap(false);

        Inputs.Player_NIS.Move.performed -= context => gameManager.PlayerController.OnMoveInput(context.ReadValue<Vector2>());
        Inputs.Player_NIS.Move.canceled -= context => gameManager.PlayerController.OnMoveInput(Vector2.zero);
        Inputs.Player_NIS.Jump.started -= _ => gameManager.PlayerController.OnJumpInput();
        Inputs.Player_NIS.Look.performed -= context => gameManager.PlayerController.OnRotateInput(context.ReadValue<Vector2>());
        Inputs.Player_NIS.Look.canceled -= context => gameManager.PlayerController.OnRotateInput(Vector2.zero);
        Inputs.Player_NIS.Zoom.performed -= context => gameManager.PlayerController.OnZoomInput(context.ReadValue<Vector2>());
        Inputs.Player_NIS.CarSummon.started -= _ => gameManager.PlayerController.OnCarSummonInput();
    }
    public void RemoveInputs(GM_Doom gameManager)
    {
        Inputs.Player_DOT.Move.started -= context => gameManager.Player.OnWalkDirectionChoose(context.ReadValue<float>(), ref gameManager.Player.WalkDirection);
        Inputs.Player_DOT.Move.performed -= context => gameManager.Player.OnMoveInput(context);
        Inputs.Player_DOT.Move.canceled -= context => gameManager.Player.OnActionStop(Actions.Move);
        Inputs.Player_DOT.Move.canceled -= _ => gameManager.Player.OnWalkDirectionChoose(0, ref gameManager.Player.WalkDirection);
        Inputs.Player_DOT.Rotate.started -= context => gameManager.Player.OnWalkDirectionChoose(context.ReadValue<float>(), ref gameManager.Player.RotateDirection);
        Inputs.Player_DOT.Rotate.performed -= context => gameManager.Player.OnRotateInput(context);
        Inputs.Player_DOT.Rotate.canceled -= _ => gameManager.Player.OnActionStop(Actions.Rotate);
        Inputs.Player_DOT.Rotate.canceled -= _ => gameManager.Player.OnWalkDirectionChoose(0, ref gameManager.Player.RotateDirection);
        Inputs.Player_DOT.Jump.performed -= context => gameManager.Player.OnJumpInput(context);
        Inputs.Player_DOT.Jump.canceled -= _ => gameManager.Player.OnActionStop(Actions.Jump);
        Inputs.Player_DOT.Pause.started -= _ => gameManager.TogglePauseMinigame();
        Inputs.Player_DOT.Move.started -= _ => gameManager.Player.OnActionChoose(Actions.Move);
        Inputs.Player_DOT.Rotate.started -= _ => gameManager.Player.OnActionChoose(Actions.Rotate);
        Inputs.Player_DOT.Jump.started -= _ => gameManager.Player.OnActionChoose(Actions.Jump);
        Inputs.Player_DOT.Move.started -= _ => gameManager.Player.OnActionChoose(Actions.Move);
        Inputs.Player_DOT.Rotate.started -= _ => gameManager.Player.OnActionChoose(Actions.Rotate);
        Inputs.Player_DOT.Jump.started -= _ => gameManager.Player.OnActionChoose(Actions.Jump);
    }
    public void RemoveInputs(GM_Tilemap gameManager)
    {
        Inputs.Player_Tilemap.Move.started -= _ => gameManager.Player.OnMoveInput(_.ReadValue<Vector2>());
        Inputs.Player_Tilemap.Move.performed -= context => gameManager.Player.OnMoveInput(context.ReadValue<Vector2>());
        Inputs.Player_Tilemap.Move.canceled -= _ => gameManager.Player.OnMoveInput(Vector2.zero);
        Inputs.Player_Tilemap.Jump.started -= context => gameManager.Player.OnJumpInput();

        Inputs.Player_Tilemap.Move.Reset();
        Inputs.Player_Tilemap.Jump.Reset();
    }
    public void RemoveInputs(GM_Advanced2D gameManager)
    {
        
    }
    #endregion
}