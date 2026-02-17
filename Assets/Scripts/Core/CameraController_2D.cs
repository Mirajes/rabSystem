using UnityEngine;

public class CameraController_2D : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, -1.7f, -25f);
    [SerializeField] private float _cameraOrthographicSize = 6.5f;
    private Transform _cameraTarget;
    
    private void Update()
    {
        if (_cameraTarget == null) return;

        ObserveCameraTarget();
    }

    public void Init(Transform startTarget)
    {
        _camera = Camera.main;
        _camera.orthographic = true;
        _camera.orthographicSize = _cameraOrthographicSize;

        ChangeCameraTarget(startTarget);
        _camera.transform.position = new Vector3(startTarget.position.x, startTarget.position.y - _cameraOffset.y, _cameraOffset.z);
    }

    public void ChangeCameraTarget(Transform newTarget)
    {
        _cameraTarget = newTarget;
    }

    private void ObserveCameraTarget()
    {
        _camera.transform.position = _cameraTarget.position - _cameraOffset;
    }

}