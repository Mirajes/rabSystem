using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Advc3D_Ticker : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _swingAngle = 90f;
    [SerializeField] private float _swingDuration = 2f;

    [SerializeField] private bool _isUseDefaultAngle = true;
    [SerializeField] private Vector3 _swingRotateAngle = Vector3.zero;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        _pivot.localEulerAngles = new Vector3(_pivot.localEulerAngles.x, _pivot.localEulerAngles.y, _swingAngle);

        StartSwing();
    }

    private void StartSwing()
    {
        if (_isUseDefaultAngle)
            _swingRotateAngle = new Vector3(_pivot.localEulerAngles.x, _pivot.localEulerAngles.y, -_swingAngle);

        Sequence swingSequence = DOTween.Sequence(); // eto ne nuzhno
        swingSequence.Append(_pivot.DORotate(_swingRotateAngle, _swingDuration)
            .SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Yoyo);
    }
}