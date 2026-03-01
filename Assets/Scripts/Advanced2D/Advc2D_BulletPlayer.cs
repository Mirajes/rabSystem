using UnityEngine;

public class Advc2D_BulletPlayer : Advc2D_Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) Destroy(this.gameObject);
        if (collision.gameObject.CompareTag("Enemy")) Destroy(collision.gameObject);
    }
}