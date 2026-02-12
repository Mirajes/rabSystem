using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Collider _deathZone3D;
    [SerializeField] private Collider2D _deathZone2D;

    [SerializeField] private string _resetSceneName;

    private void OnTriggerEnter(Collider collision)
    {
        if (_deathZone3D == null) return;

        if (collision.tag == "Player")
            SceneManager.LoadScene(_resetSceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_deathZone2D == null) return;

        if (collision.tag == "Player")
            SceneManager.LoadScene(_resetSceneName);
    }
}
