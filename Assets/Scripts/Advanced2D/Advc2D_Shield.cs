using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Advc2D_Shield : MonoBehaviour
{
    private CircleCollider2D _circleCollider;

    [SerializeField] private float _explodingPower = 50f;
    [SerializeField] private bool _isExploding = false;
    [SerializeField] private float _timeToDisappear = 2f;

    private CancellationTokenSource _cts;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isExploding)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D enemyCol);

                if (enemyCol != null)
                {
                    Vector2 direction = this.transform.position - collision.transform.position;
                    enemyCol.AddForce(-direction.normalized * _explodingPower, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnEnable()
    {
        _cts = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public void SetExploding(bool isExploding)
    {
        _isExploding = isExploding;

        DestroyShield(_cts.Token).Forget();
    }

    async UniTask DestroyShield(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDisappear), cancellationToken: token);
        SetExploding(false);
        this.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
        this.gameObject.SetActive(false);

    }
}
