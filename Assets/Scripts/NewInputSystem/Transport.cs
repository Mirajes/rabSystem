using UnityEngine;

public abstract class Transport : MonoBehaviour
{
    [SerializeField] protected TransportData _Data;
    protected Rigidbody _Rb;

    protected bool _IsPlayerControlling = false;
    protected Vector2 _MoveDirection;

    public void OnControllerInput(Vector2 direction)
    {
        _MoveDirection = new Vector2(direction.x, direction.y);
    }

    protected void ApplyRotation()
    {
        this.transform.localEulerAngles += new Vector3(0, _MoveDirection.x, 0);
    }

    protected abstract void ApplyMovement();

    private void OnEnable()
    {
        _Rb = GetComponentInChildren<Rigidbody>();
    }
}
