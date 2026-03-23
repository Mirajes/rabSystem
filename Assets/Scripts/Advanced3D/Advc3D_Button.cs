using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public abstract class Advc3D_Button : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] protected Transform _ButtonTransform;
    [SerializeField] protected Collider _ButtonCollider;
    [SerializeField] protected float _ButtonPressureForce = 0.3f;
    [SerializeField] protected bool _IsPressed = false;
    [SerializeField] protected LayerMask _EntityLayerMask = 7;
    [SerializeField] protected string _Key;
    [SerializeField] protected Advc3D_InteractableObject _keyBox;

    [Header("Visual")]
    [SerializeField] protected Renderer _ButtonRenderer;
    [SerializeField] protected Material _BaseMaterial;
    [SerializeField] protected Material _IncorrentMaterial;
    private CancellationTokenSource _cts;

    public string Key => _Key;


    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_IsPressed) return;

        if (_Key == "") // a dlya chego?
        {
            _IsPressed = true;
            print("no key");
            ButtonPressed();
        }
        else if (other.gameObject == _keyBox.gameObject) // zachem key
        {
            _IsPressed = true;
            print("key correct");
            ButtonPressed();
        }
        else
        {
            print("incorrect key");
            _cts = new();
            ShowIncorrect(_cts.Token).Forget();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (_IsPressed && !IsEntityInside())
        {
            ButtonReleased();
            _IsPressed = false;
        }
    }
    protected virtual void ButtonPressed()
    {
        _ButtonTransform.localScale = new Vector3(
            _ButtonTransform.localScale.x,
            _ButtonPressureForce,
            _ButtonTransform.localScale.z);
    }
    protected virtual void ButtonReleased()
    {
        _ButtonTransform.localScale = Vector3.one;
    }

    private bool IsEntityInside()
    {
        Collider[] hit = Physics.OverlapBox(transform.position, _ButtonCollider.transform.localScale * 0.5f, Quaternion.identity, _EntityLayerMask);
        print($"colliders inside -- {hit.Length}");
        return hit.Length > 0;
    }

    private async UniTask ShowIncorrect(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        float duration = 1f;

        _ButtonRenderer.material = _IncorrentMaterial;

        await UniTask.Delay(TimeSpan.FromSeconds(duration));
        _ButtonRenderer.material = _BaseMaterial;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _ButtonCollider.transform.localScale);
    }


}