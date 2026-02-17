using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CameraController_2D))]
public class GM_Advanced2D : GameManagerBase
{
    // + CameraController_2D from Core folder
    public Advc2D_PlayerController Player => _player;

    [SerializeField] private Transform _spawnPos;
    private GameObject _playerPrefab;
    private Advc2D_PlayerController _player;

    private CameraController_2D _cameraController;

    private void Start()
    {
        Init();

    }

    protected override void Init()
    {
        _playerPrefab = Resources.Load<GameObject>("KT_Advc2D/Player");
        GameObject newPlayer = Instantiate(_playerPrefab, _spawnPos);

        _player = newPlayer.GetComponent<Advc2D_PlayerController>();

        Debug.Log("try to init");
        Initializer.Instance.Advc2D_InitPlayerController(this);


        _cameraController = GetComponent<CameraController_2D>();
        _cameraController.Init(_player.transform);
    }
}

public class Advc2D_IntetactableObject : MonoBehaviour
{

}

public class Advc2D_Box : Advc2D_IntetactableObject
{
    [SerializeField] private float _hp = math.INFINITY;
    [SerializeField] private bool _isMovable = false;
}

public class Advc2D_Bullet { }

public abstract class Advc2D_Enemy : MonoBehaviour
{
    protected virtual void Attack() { print("pew"); }
}

public class Advc2D_ChasingEnemy : Advc2D_Enemy { }

public class Advc2D_TurretEnemy : Advc2D_Enemy { }

public class Advc2D_ShooterEnemy : Advc2D_Enemy { }