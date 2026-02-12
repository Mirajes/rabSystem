using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [SerializeField] private Collider _winZone3D;
    [SerializeField] private Collider2D _winZone2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_winZone2D == null) return;

        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_winZone3D == null) return;

        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
