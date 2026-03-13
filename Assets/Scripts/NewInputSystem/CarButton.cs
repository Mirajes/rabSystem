using UnityEngine;

public class CarButton : NIS_Interactable
{
    [SerializeField] private Transport _transport;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        _GM.ChangeMap(true);
        _transport.enabled = true;

        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;

        _GM.ChangeMap(false);
        _transport.enabled = false;

        other.transform.parent = null;
    }

    public override void Interact()
    {
        
    }
}
