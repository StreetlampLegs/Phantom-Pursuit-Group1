using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] GameObject MazeCellPrefab;

    public float CellSize = 1f;

    private MazeGenerator _mazeGenerator;
    private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        if (!_mazeGenerator) _mazeGenerator = GetComponent<MazeGenerator>();
        if (!_navMeshSurface) _navMeshSurface = GetComponent<NavMeshSurface>();

        MazeCell[,] maze = _mazeGenerator.GetMaze();

        for (int x = 0; x < _mazeGenerator.mazeWidth; x++)
        {
            for (int y = 0; y < _mazeGenerator.mazeHeight; y++)
            {
                GameObject newCell = Instantiate(MazeCellPrefab, new Vector3(x * CellSize, 0f, y * CellSize), Quaternion.identity, transform);

                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;

                bool right = false;
                bool bottom = false;

                if (x == _mazeGenerator.mazeWidth - 1) right = true;
                if (y == 0) bottom = true;

                mazeCell.Init(top, bottom, right, left);
            }
        }

        _navMeshSurface.BuildNavMesh();
    }
}
