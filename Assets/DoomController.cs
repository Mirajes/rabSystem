using UnityEngine;

public class DoomController : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraOffset = new Vector3(-0.5f, 3f, 2f);
    private Transform _cameraParent;
    private float _cameraVerticalAngle = 0f;
    private Camera _camera;

    [SerializeField] private float _cameraSens;

    public void OnMoveInput(Vector2 input)
    {

    }

    public void OnRotateInput(Vector2 input)
    {
        float mouseX = input.x * _cameraSens;
        float mouseY = input.y * _cameraSens;

        _cameraVerticalAngle -= mouseY;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -90f, 90f);

        _cameraParent.localEulerAngles = new Vector3(_cameraVerticalAngle, 0f, 0f);

        this.transform.Rotate(Vector3.up * mouseX);
    }

    private void Start()
    {
        _camera = Camera.main;
        _camera.transform.parent = _cameraParent;
        _camera.transform.localPosition = _cameraOffset;
    }
}
