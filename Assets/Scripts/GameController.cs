using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _monster;
    [SerializeField]
    private bool _spawnMonster = true;


    private NavMeshSurface _navMeshSurface;


    private void Awake()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        _navMeshSurface.BuildNavMesh();
        SpawnEntities();
    }

    void SpawnEntities()
    {
        // Spawn player at 0, 0
        if (_player)
        {
            NavMesh.SamplePosition(new Vector3(0, 0, 0), out NavMeshHit playerSpawnPosition, 1f, NavMesh.AllAreas);
            Instantiate(_player, playerSpawnPosition.position, Quaternion.identity);
        }

        if (_spawnMonster && _monster)
        {
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        Vector3 mazeCenter = new Vector3(10, 0, 10);

        Vector3 monsterSpawnPosition = GetNearestNavMeshPosition(mazeCenter, 12);
        Instantiate(_monster, monsterSpawnPosition, Quaternion.identity);
    }

    Vector3 GetNearestNavMeshPosition(Vector3 position, float radius)
    {
        Vector3 randomPosition;
        while (true)
        {
            randomPosition = Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(position + randomPosition, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void LoseGame()
    {
        // Pause game & show UI
        Debug.Log("You lose");
    }

    public void WinGame()
    {
        // Pause game & show UI
        Debug.Log("You win");
    }
}
