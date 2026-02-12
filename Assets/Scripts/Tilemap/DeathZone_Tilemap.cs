using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone_Tilemap : MonoBehaviour
{
    private Collider2D _deathzoneCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("Tilemap");
        }
    }

    private void Awake()
    {
        _deathzoneCollider = GetComponent<Collider2D>();
    }
}
