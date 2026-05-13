using UnityEngine;

public class ProB_Enemy : MonoBehaviour, ISavable, IInteractive
{
    [SerializeField] private bool _isScared;
    [SerializeField] private SpriteRenderer _face;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private ProB_Skulls _skull = ProB_Skulls.No;

    private string _id;

    private const string _posParamName = "_pos";
    private const string _materialParamName = "_material";

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

    public void OnSave(SaveData data)
    {
        data.Data.Add(_id, JsonUtility.ToJson(_isScared));
        data.Data.Add(_id + _posParamName, JsonUtility.ToJson(transform.position));
        data.Data.Add(_id + _materialParamName, JsonUtility.ToJson(_renderer.material));
    }

    public void OnLoad(SaveData data)
    {
        if (!data.Data.ContainsKey(_id))
            return;

        SetFear(JsonUtility.FromJson<bool>(data.Data[_id]));
        transform.position = JsonUtility.FromJson<Vector3>(data.Data[_id +_posParamName]);
        _renderer.material = JsonUtility.FromJson<Material>(data.Data[_id + _materialParamName]);
    }

    public void HandleInteractive()
    {
        
    }
}
