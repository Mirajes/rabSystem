using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Tilemap : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    private Vector2 _moveDirection = Vector2.zero;

    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private int _maxJumps = 1;
}
