using UnityEngine;

public class Car : Transport
{

    protected override void ApplyMovement()
    {
        transform.localPosition += this.transform.right * -_MoveDirection.y * _Data.Speed * Time.deltaTime;
        //_Rb.AddForce(this.transform.right * -_MoveDirection.y * _Data.Speed, UnityEngine.ForceMode.Impulse);
    }

    private void Update()
    {
        ApplyRotation();
        ApplyMovement();
    }
}
