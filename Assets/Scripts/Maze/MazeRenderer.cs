using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using static MazeCellObject;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _mazeCellPrefab;
    [SerializeField] private float _cellSize = 1f;
    public float CellSize
    {
        get => _cellSize;
    }

    private MazeGenerator _mazeGenerator;
    private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        InitializeComponents();
        RenderMaze();
        _navMeshSurface.BuildNavMesh();
        BroadcastMessage("SpawnEntities");
    }

    private void InitializeComponents()
    {
        _mazeGenerator = GetComponent<MazeGenerator>();
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void RenderMaze()
    {
        MazeCell[,] maze = _mazeGenerator.GenerateMaze();

        for (int x = 0; x < _mazeGenerator.MazeWidth; x++)
        {
            for (int y = 0; y < _mazeGenerator.MazeHeight; y++)
            {
                CreateMazeCell(maze, x, y);
            }
        }
    }

    private void CreateMazeCell(MazeCell[,] maze, int x, int y)
    {
        Vector3 position = new Vector3(x * _cellSize, 0f, y * _cellSize);
        GameObject newCell = Instantiate(_mazeCellPrefab, position, Quaternion.identity, transform);
        MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

        bool top = maze[x, y].TopWall;
        bool left = maze[x, y].LeftWall;
        bool ground = maze[x, y].Floor;
        bool right = maze[x, y].Visited && x == _mazeGenerator.MazeWidth - 1;
        bool bottom = maze[x,y].Visited && y == 0;

        if (!maze[x, y].Visited)
        {
            ground = false;
            left = !(x == 0 || !maze[x - 1, y].Visited);
            top = !(y == _mazeGenerator.MazeHeight - 1 || !maze[x, y + 1].Visited);
        }

        WallState wallState = new WallState(top, bottom, right, left, ground);

        mazeCell.Init(wallState);
    }
}
