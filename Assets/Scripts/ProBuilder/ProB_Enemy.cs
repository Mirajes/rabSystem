using UnityEngine;

public class ProB_Enemy : MonoBehaviour, ISavable, IInteractive
{
    [SerializeField] private bool _isScared;
    [SerializeField] private SpriteRenderer _face;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private ProB_Skulls _skull = ProB_Skulls.No;

    private string _id;



    private void SetFear(bool isScared)
    {
        _isScared = isScared;

        if (!_isScared)
        {
            _face.sprite = ProB_GameContext.Instance.CalmSprite;
        }
        else
        {
            _face.sprite = ProB_GameContext.Instance.ScareSprite;
        }
        
        GM_ProBuilder.Instance.SaveManager.Save();
    }

    private void OnEnable()
    {
        ProB_SaveManager.OnSave += OnSave;
        ProB_SaveManager.OnLoad += OnLoad;

        _id = gameObject.GetInstanceID().ToString();
    }

    private void OnDisable()
    {
        ProB_SaveManager.OnSave -= OnSave;
        ProB_SaveManager.OnLoad -= OnLoad;
    }

    private void OnMouseDown()
    {
        print("oi");
    }

    public void OnSave(SaveData data)
    {
        Debug.Log(_renderer.material);

        Vector3 position = transform.position;
        string matName = ProB_GameContext.Instance.GetKeyByMaterial(_renderer.material);

        data.Data.Add(_id, JsonUtility.ToJson(_isScared));
        data.Data.Add(_id + ProB_GameContext.Instance.PosParamName, JsonUtility.ToJson(transform.position));
        data.Data.Add(_id + ProB_GameContext.Instance.MaterialParamName, JsonUtility.ToJson(matName));

        Debug.Log($"[Enemy] - Saved to {transform.position}");
    }

    public void OnLoad(SaveData data)
    {
        if (!data.Data.ContainsKey(_id))
            return;

        Material material = ProB_GameContext.Instance.GetMaterialByName(
            JsonUtility.FromJson<string>
            (data.Data[_id + ProB_GameContext.Instance.MaterialParamName]));

        SetFear(JsonUtility.FromJson<bool>(data.Data[_id]));
        transform.position = JsonUtility.FromJson<Vector3>(data.Data[_id + ProB_GameContext.Instance.PosParamName]);
        _renderer.material = material;

        Debug.Log($"[Enemy] - Loaded at {JsonUtility.FromJson<Vector3>(data.Data[_id + ProB_GameContext.Instance.PosParamName])}");
    }

    public void HandleInteractive()
    {
        
    }
}
