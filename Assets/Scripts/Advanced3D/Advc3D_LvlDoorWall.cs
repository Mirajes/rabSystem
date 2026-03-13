using DG.Tweening;
using UnityEngine;

public class Advc3D_LvlDoorWall : Advc3D_InteractableObject
{
    [SerializeField] private float _moveTime = 3f;

    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _endPosOffset;

    private Tween _moveTween;

    private void OnEnable()
    {
        _startPos = transform.position;
    }

    public override void Interact()
    {
        _moveTween.Kill();
        _moveTween = this.transform.DOMove(_startPos + _endPosOffset, _moveTime);
    }
    
    public override void Restart()
    {
        _moveTween.Kill();
        _moveTween = this.transform.DOMove(_startPos, _moveTime);
    }
}
