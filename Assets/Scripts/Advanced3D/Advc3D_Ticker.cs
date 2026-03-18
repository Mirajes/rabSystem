using UnityEngine;
using DG.Tweening;

public class Advc3D_Ticker : MonoBehaviour
{
    public Transform pivot; // точка, вокруг которой маятник качается
    public float swingDistance = 2f; // насколько в стороны
    public float swingDuration = 2f; // время одного полного качания
    public float recoilForce = 5f; // сила отскока при столкновении

    private Rigidbody rb;
    private bool isRecoiling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        StartSwing();
    }

    private void StartSwing()
    {
        // Бесконечное качание из стороны в сторону
        Sequence swingSequence = DOTween.Sequence();
        swingSequence.Append(transform.DOLocalMoveX(swingDistance, swingDuration).SetEase(Ease.InOutSine))
                     .Append(transform.DOLocalMoveX(-swingDistance, swingDuration).SetEase(Ease.InOutSine))
                     .SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRecoiling)
        {
            // Оттолкнуть маятник в противоположную сторону
            Vector3 direction = (transform.position - other.transform.position).normalized;
            Recoil(direction);
        }
    }

    private void Recoil(Vector3 direction)
    {
        isRecoiling = true;
        // Остановить текущие анимации
        DOTween.Kill(transform);
        // Можно добавить небольшой эффект отскока
        Vector3 recoilTarget = transform.position + direction * recoilForce;
        // Переместиться к recoilTarget
        transform.DOMove(recoilTarget, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            // Вернуться к качанию
            StartSwing();
            isRecoiling = false;
        });
    }
}