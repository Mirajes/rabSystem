using UnityEngine;

public class ProB_GameContext : GameContext<ProB_GameContext>
{
    [SerializeField] private Sprite _scareSprite;
    [SerializeField] private Sprite _calmSprite;

    public Sprite ScareSprite => _scareSprite;
    public Sprite CalmSprite => _calmSprite;
}