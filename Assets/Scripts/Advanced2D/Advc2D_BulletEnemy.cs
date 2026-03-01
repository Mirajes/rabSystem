using UnityEngine;

public class Advc2D_BulletEnemy : Advc2D_Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) Destroy(this.gameObject);
        if (collision.gameObject.CompareTag("Player")) { Destroy(this.gameObject); GM_Advanced2D.PlayerHit?.Invoke();  }
    }
}
