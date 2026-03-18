using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Advc3D_CameraController : MonoBehaviour
{
    public Camera MainCamera => _mainCamera;
    public int LastViewPos => _lastViewPos;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Vector3 _firstViewOffset = new Vector3(0f, 0f, 1f);

    private Transform _playerTransform;
    private bool _isFirstView = false;
    private int _lastViewPos = 0;
    private List<Transform> _cameraPoses;

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public void OnSwitchViewInput(InputAction.CallbackContext context)
    {
        _isFirstView = !_isFirstView;

        if (_isFirstView)
        {
            _mainCamera.transform.parent = _playerTransform;
            _mainCamera.transform.localPosition = _playerTransform.position + _firstViewOffset;
        }
        else
        {
            _mainCamera.transform.parent = null;
            _mainCamera.transform.position = _cameraPoses[_lastViewPos].position;
        }
    }

    public void OnPrevPosInput(InputAction.CallbackContext context)
    {
        _lastViewPos--;
        if (_lastViewPos < 0)
            _lastViewPos = _cameraPoses.Count;

        _mainCamera.transform.position = _cameraPoses[_lastViewPos].position;
    }

    public void OnNextPosInput(InputAction.CallbackContext context)
    {
        _lastViewPos++;
        if (_lastViewPos >  _cameraPoses.Count)
            _lastViewPos = 0;

        _mainCamera.transform.position = _cameraPoses[_lastViewPos].position;
    }

    public void OnLevelChange(List<Transform> cameraPoses)
    {
        _cameraPoses = cameraPoses;
        _lastViewPos = 0;
    }

    private void OnEnable()
    {
        _mainCamera = Camera.main;
    }
}
