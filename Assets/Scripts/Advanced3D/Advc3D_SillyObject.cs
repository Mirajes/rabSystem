using DG.Tweening;
using UnityEngine;

public class Advc3D_SillyObject : MonoBehaviour
{
    [SerializeField] private bool _isEnabled = true;

    private Vector3 _startPos;

    [Header("Spin")]
    [SerializeField] private float _spinSpeed = 2f;
    [SerializeField] private Vector3 _spinTweenOffset = new Vector3(0f, 180f, 0f);
    [Header("Bounce")]
    [SerializeField] private float _bounceSpeed = 1.0f;
    [SerializeField] private Vector3 _bounceTweenOffset = new Vector3(0f, 1f, 0f);

    private Tween _spinTween;
    private Tween _bounceTween;

    private void OnEnable()
    {
        if (!_isEnabled) return;

        _startPos = transform.position;

        _spinTween = this.transform.DORotate(
            transform.eulerAngles + _spinTweenOffset,
            _spinSpeed).SetLoops(-1, LoopType.Incremental);

        _bounceTween = this.transform.DOMove(
            transform.position + _bounceTweenOffset,
            _bounceSpeed).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        this.transform.position = _startPos;
        this.transform.rotation = Quaternion.identity;

        _spinTween.Kill();
        _bounceTween.Kill();
    }
}