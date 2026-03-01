using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public abstract class Advc2D_Enemy : MonoBehaviour
{
    protected Rigidbody2D _RigidBody;

    private void OnEnable()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Attack() { }
}

//public class Advc2D_TurretEnemy : Advc2D_Enemy { }

//public class Advc2D_ShooterEnemy : Advc2D_Enemy { }