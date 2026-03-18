using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

public class Advc3D_DeathButton : Advc3D_Button
{
    [Header("lvl")]
    [SerializeField] protected Animator _animator;
    private CancellationTokenSource _cts;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent<Collider>(out Collider col);
            if (col != null)
            {
                _cts = new();
                _animator.Play("advc3d_DeathButton");

                AnimWaiter(_cts.Token).Forget();
            }
        }
    }

    private async UniTask AnimWaiter(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        // ∆дЄм, пока текуща€ анимаци€ не завершитс€
        while (true)
        {
            token.ThrowIfCancellationRequested();

            var state = _animator.GetCurrentAnimatorStateInfo(0);
            if (state.IsName("advc3d_DeathButton") && state.normalizedTime >= 1f)
            {
                break;
            }
            await UniTask.Delay(100);
        }

        transform.DOMoveY(transform.position.y - 5f, 1f);
        await UniTask.Delay(1000);


        if (_cts != null)
        {
            if (!_cts.IsCancellationRequested)
                _cts.Cancel();

            _cts.Dispose();
            _cts = null;
        }

        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}