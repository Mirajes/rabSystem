using UnityEngine;

public class ProB_GameContext : GameContext<ProB_GameContext>
{
    [SerializeField] private Sprite _scareSprite;
    [SerializeField] private Sprite _calmSprite;
    [SerializeField] private Material _scareMaterial;
    [SerializeField] private Material _calmMaterial;

    public Sprite ScareSprite => _scareSprite;
    public Sprite CalmSprite => _calmSprite;
    public Material ScareMaterial => _scareMaterial;
    public Material CalmMaterial => _calmMaterial;

    public readonly string MaterialParamName = "_material";
    public readonly string PosParamName = "_pos";
}