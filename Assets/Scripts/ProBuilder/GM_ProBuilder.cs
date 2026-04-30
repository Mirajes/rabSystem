using UnityEngine;

public class GM_ProBuilder : GameManagerBase
{
    [SerializeField] private PB_PlayerController _player;

    public PB_PlayerController Player => _player;

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        Initializer.Instance.RemoveInputs(this);
    }

    protected override void Init()
    {
        Initializer.Instance.ProB_InitPlayerController(this);
        Initializer.Instance.EnableInputs();
    }

}

/*
Unity Cinemachine is existng
*/