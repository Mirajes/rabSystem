using DG.Tweening;
using UnityEngine;

public class DaynLogic_Tilemap : MonoBehaviour
{
    [SerializeField] private Collider2D _enterCollider;
    [SerializeField] private Collider2D _exitCollider;

    [SerializeField] private float _fadeTime;
    [SerializeField] private SpriteRenderer _dayn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouching(_enterCollider))
        {
            _dayn.DOFade(1f, _fadeTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_exitCollider) // how to check?
        {
            _dayn.DOFade(0f, _fadeTime);
        }
    }

    private void Awake()
    {
        _dayn = this.transform.GetComponent<SpriteRenderer>();

        _dayn.color = new Color(_dayn.color.r, _dayn.color.g, _dayn.color.b, 0f);
    }
}
