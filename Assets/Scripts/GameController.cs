using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject monster;

    private MazeRenderer _mazeRenderer;
    private MazeGenerator _mazeGenerator;

    void Awake()
    {
        InitializeComponents();
    }

    void InitializeComponents()
    {
        _mazeRenderer = GetComponent<MazeRenderer>();
        _mazeGenerator = GetComponent<MazeGenerator>();
    }

    void SpawnEntities()
    {
        // Spawn player at 0, 0
        if (player)
        {
            NavMesh.SamplePosition(new Vector3(0, 0, 0), out NavMeshHit playerSpawnPosition, 1f, NavMesh.AllAreas);
            Instantiate(player, playerSpawnPosition.position, Quaternion.identity);
        }

        // Spawn monster in the middle of the maze

        Vector3 mazeCenter = new Vector3(_mazeRenderer.CellSize * _mazeGenerator.MazeWidth / 2, 0, _mazeRenderer.CellSize * _mazeGenerator.MazeHeight / 2);

        Vector3 monsterSpawnPosition = GetNearestNavMeshPosition(mazeCenter, _mazeRenderer.CellSize * 4);
        Instantiate(monster, monsterSpawnPosition, Quaternion.identity);
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
