using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ProB_GameContext : MonoBehaviour
{
    [SerializeField] private Sprite _scareSprite;
    [SerializeField] private Sprite _calmSprite;

    [SerializeField]
    [SerializedDictionary("MaterialKey", "Material")]
    private SerializedDictionary<ProB_State, Material> _materialDictionary = new();

    public Sprite ScareSprite => _scareSprite;
    public Sprite CalmSprite => _calmSprite;

    public readonly string MaterialParamName = "_material";
    public readonly string PosParamName = "_pos";
    public readonly string StateParamName = "_state";

    public static ProB_GameContext Instance => _instance;
    private static ProB_GameContext _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public Material GetMaterialByState(ProB_State state)
    {
        if (!_materialDictionary.ContainsKey(state))
        {
            Debug.LogWarning($"[GameContext] - No {state} for material");
            return null;
        }

        return _materialDictionary[state];
    }
}