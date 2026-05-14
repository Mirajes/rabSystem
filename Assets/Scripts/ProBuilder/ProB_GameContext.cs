using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ProB_GameContext : MonoBehaviour
{
    [SerializeField] private Sprite _scareSprite;
    [SerializeField] private Sprite _calmSprite;

    [SerializeField]
    [SerializedDictionary("MaterialKey", "Material")]
    private SerializedDictionary<string, Material> _materialDictionary = new();
    private SerializedDictionary<Material, string> _reversedMaterialDictionary = new();

    public Sprite ScareSprite => _scareSprite;
    public Sprite CalmSprite => _calmSprite;

    public readonly string MaterialParamName = "_material";
    public readonly string PosParamName = "_pos";

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

        BuildReversedMaterialDictionary();
    }

    private void BuildReversedMaterialDictionary()
    {
        _reversedMaterialDictionary.Clear();
        foreach (var item in _materialDictionary)
        {
            _reversedMaterialDictionary[item.Value] = item.Key;
        }
    }

    public Material GetMaterialByName(string key)
    {
        if (!_materialDictionary.ContainsKey(key)) 
        {
            Debug.LogWarning($"[GameContext] - No {key} for material");
            return null;
        }

        return _materialDictionary[key];
    }

    public string GetKeyByMaterial(Material mat)
    {
        if (!_reversedMaterialDictionary.ContainsKey(mat))
        {
            Debug.LogWarning($"[GameContext] - No {mat} for keyString");
            return null;
        }

        return _reversedMaterialDictionary[mat];
    }
}