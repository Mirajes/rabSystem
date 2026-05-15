using UnityEngine;

public class ProB_Enemy : MonoBehaviour, ISavable, IInteractive
{
    [SerializeField] private bool _isScared;
    [SerializeField] private SpriteRenderer _face;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private ProB_Skulls _skull = ProB_Skulls.No;
    [SerializeField] private ProB_State _state = ProB_State.Calm;

    private string _id;



    private void SetFear(bool isScared)
    {
        _isScared = isScared;

        if (!_isScared)
        {
            _face.sprite = ProB_GameContext.Instance.CalmSprite;
            _state = ProB_State.Calm;
        }
        else
        {
            _face.sprite = ProB_GameContext.Instance.ScareSprite;
            _state = ProB_State.Scared;
        }
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

        data.Data.Add(_id, JsonUtility.ToJson(_isScared));
        data.Data.Add(_id + ProB_GameContext.Instance.PosParamName, JsonUtility.ToJson(position));

        Debug.Log($"[Enemy] - Saved to {position}");
    }

    public void OnLoad(SaveData data)
    {
        if (!data.Data.ContainsKey(_id))
            return;

        bool isScared = JsonUtility.FromJson<bool>(data.Data[_id]);
        Vector3 position = JsonUtility.FromJson<Vector3>(data.Data[_id + ProB_GameContext.Instance.PosParamName]);
        Material material = ProB_GameContext.Instance.GetMaterialByState(_state);

        SetFear(isScared);
        transform.position = position;
        _renderer.material = material;

        Debug.Log($"[Enemy] - Loaded at {position}");
    }

    public void HandleInteractive()
    {
        
    }
}
