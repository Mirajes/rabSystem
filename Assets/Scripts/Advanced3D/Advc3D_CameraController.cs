using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Advc3D_CameraController : MonoBehaviour
{
    public Camera MainCamera => _mainCamera;
    public int LastViewPos => _lastViewPos;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Vector3 _firstViewOffset = new Vector3(0f, 0f, 1f);
    [SerializeField] private float _tweenSpeed = 1f;

    private Transform _playerTransform;
    private bool _isFirstView = false;
    private int _lastViewPos = 0;
    private List<Transform> _cameraPoses;

    private void OnEnable()
    {
        _mainCamera = Camera.main;
    }
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
            Vector3 worldOffset = _playerTransform.rotation * _firstViewOffset;
            _mainCamera.transform.position = _playerTransform.position + worldOffset;
            _mainCamera.transform.rotation = _playerTransform.rotation;
        }
        else
        {
            var cameraTransform = _cameraPoses[_lastViewPos];
            _mainCamera.transform.parent = null;
            _mainCamera.transform.position = cameraTransform.position;
            _mainCamera.transform.localEulerAngles = cameraTransform.localEulerAngles;
        }
    }

    public void OnPrevPosInput(InputAction.CallbackContext context)
    {
        _lastViewPos--;
        if (_lastViewPos < 0)
            _lastViewPos = _cameraPoses.Count - 1;

        ChangeCameraTransform();
    }

    public void OnNextPosInput(InputAction.CallbackContext context)
    {
        _lastViewPos++;
        if (_lastViewPos >=  _cameraPoses.Count)
            _lastViewPos = 0;

        ChangeCameraTransform();
    }


    public void OnLevelChange(List<Transform> cameraPoses)
    {
        _cameraPoses = cameraPoses;
        _lastViewPos = 0;

        ChangeCameraTransform();
    }

    private void ChangeCameraTransform()
    {
        //_mainCamera.transform.rotation = _cameraPoses[_lastViewPos].rotation;
        //_mainCamera.transform.position = _cameraPoses[_lastViewPos].position;

        DOTween.Kill(_mainCamera.transform);
        _mainCamera.transform.DOMove(_cameraPoses[_lastViewPos].position, _tweenSpeed).SetEase(Ease.OutSine);
        _mainCamera.transform.DORotateQuaternion(_cameraPoses[_lastViewPos].rotation, _tweenSpeed).SetEase(Ease.OutSine);
    }
}