using UnityEngine;

public class Advc3D_Coin : MonoBehaviour
{
    public bool IsCollected => _isCollected;
    public int CoinValue => _coinValue;
    public int Index => _index;

    [SerializeField] private int _coinValue = 1;
    [SerializeField] private bool _isCollected = false;
    [SerializeField] private int _index = 0;

    public void OnCoinCollect()
    {
        _isCollected = true;
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GM_Advc3D.CoinCollect?.Invoke(this);
        }
    }
}
