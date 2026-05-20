using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ProB_SO_Item : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _description;

    public string Name => _name;
    public int Id => _id;
    public Sprite Sprite => _sprite;
    public string Description => _description;
}