using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Advc2D_ShieldStation : Advc2D_IntetactableObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Advc2D_Player>().CreateShield();
        }
    }
}
