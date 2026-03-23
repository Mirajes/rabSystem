using DG.Tweening;
using UnityEngine;

/*
rabotaet uzhasno
ne vorovat'
na 180 lomaetca
*/

[RequireComponent(typeof(Rigidbody))]
public class Advc3D_Ticker : MonoBehaviour // mayatnik
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _swingAngle = 90f;
    [SerializeField] private float _swingDuration = 2f;

    [SerializeField] private bool _isUseDefaultAngle = true;
    [SerializeField] private Vector3 _swingRotateAngle = Vector3.zero;

    private Rigidbody _rb;
    private Sequence _swingTween;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        _pivot.localEulerAngles = new Vector3(_pivot.localEulerAngles.x, _pivot.localEulerAngles.y, _swingAngle);
    }

    private void OnEnable()
    {
        StartSwing();
    }

    private void OnDisable()
    {
        DOTween.Kill(_pivot);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GM_Advc3D.Restart?.Invoke();
    }

    private void StartSwing()
    {
        if (_isUseDefaultAngle)
            _swingRotateAngle = new Vector3(_pivot.localEulerAngles.x, _pivot.localEulerAngles.y, -_swingAngle);

        _swingTween = DOTween.Sequence(_pivot); // eto ne nuzhno
        _swingTween.Append(_pivot.DORotate(_swingRotateAngle, _swingDuration)
            .SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Yoyo);
    }
}