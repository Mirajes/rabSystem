using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public abstract class Advc2D_Enemy : MonoBehaviour
{
    protected virtual void Attack() { print("pew"); }
}

//public class Advc2D_TurretEnemy : Advc2D_Enemy { }

//public class Advc2D_ShooterEnemy : Advc2D_Enemy { }