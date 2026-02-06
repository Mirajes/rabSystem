using UnityEngine;

public class CameraController_Tilemap : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, -1.7f, -25f);
    private Transform _cameraTarget;
    
    private void Update()
    {
        if (_cameraTarget == null) return;

        ObserveCameraTarget();
    }

    public void Init(Transform startTarget)
    {
        _camera = Camera.main;
        ChangeCameraTarget(startTarget);
        _camera.transform.position = new Vector3(startTarget.position.x, startTarget.position.y - _cameraOffset.y, _cameraOffset.z);
    }

    public void ChangeCameraTarget(Transform newTarget)
    {
        _cameraTarget = newTarget;
    }

    private void ObserveCameraTarget()
    {
        _camera.transform.position = new Vector3(_cameraTarget.position.x, _cameraTarget.position.y - _cameraOffset.y, _cameraOffset.z);
    }

}