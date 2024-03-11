using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField, Range(5, 500)]
    private int _mazeWidth = 5;
    public int MazeWidth
    {
        get => _mazeWidth;
    }

    [SerializeField, Range(5, 500)]
    private int _mazeHeight = 5;
    public int MazeHeight
    {
        get => _mazeHeight;
    }

    [SerializeField]
    private int _startX, _startY;
    [SerializeField]
    private int _minCorridorLength = 1;

    private MazeCell[,] _maze;
    public MazeCell[,] Maze
    {
        get => _maze;
    }

    private Vector2Int _currentCell;

    public MazeCell[,] GenerateMaze()
    {
        _maze = new MazeCell[_mazeWidth, _mazeHeight];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int y = 0; y < _mazeHeight; y++)
            {
                _maze[x, y] = new MazeCell(x, y);
            }
        }

        CarvePath(_startX, _startY);

        return _maze;
    }

    readonly List<Direction> directions = new()
    {
        Direction.Up,
        Direction.Down,
        Direction.Left,
        Direction.Right
    };

    List<Direction> GetRandomDirections()
    {
        List<Direction> dir = new(directions);

        List<Direction> randomDir = new();

        while (dir.Count > 0)
        {
            int index = Random.Range(0, dir.Count);
            randomDir.Add(dir[index]);
            dir.RemoveAt(index);
        }

        return randomDir;
    }

    bool IsCellValid(int x, int y)
    {
        return !(x < 0 || x > _mazeWidth - 1 || y < 0 || y > _mazeHeight - 1 || _maze[x, y].Visited);
    }


    Vector2Int CheckNeighbours()
    {
        List<Direction> randomDir = GetRandomDirections();
        Vector2Int bestNeighbour = _currentCell;

        for (int i = 0; i < randomDir.Count; i++)
        {
            Vector2Int neighbour = _currentCell;
            int corridorLength = 0;

            switch (randomDir[i])
            {
                case Direction.Up:
                    neighbour.y++;
                    while (IsCellValid(neighbour.x, neighbour.y))
                    {
                        corridorLength++;
                        if (corridorLength >= _minCorridorLength)
                            break;
                        neighbour.y++;
                    }
                    break;
                case Direction.Down:
                    neighbour.y--;
                    while (IsCellValid(neighbour.x, neighbour.y))
                    {
                        corridorLength++;
                        if (corridorLength >= _minCorridorLength)
                            break;
                        neighbour.y--;
                    }
                    break;
                case Direction.Left:
                    neighbour.x--;
                    while (IsCellValid(neighbour.x, neighbour.y))
                    {
                        corridorLength++;
                        if (corridorLength >= _minCorridorLength)
                            break;
                        neighbour.x--;
                    }
                    break;
                case Direction.Right:
                    neighbour.x++;
                    while (IsCellValid(neighbour.x, neighbour.y))
                    {
                        corridorLength++;
                        if (corridorLength >= _minCorridorLength)
                            break;
                        neighbour.x++;
                    }
                    break;
            }

            if (corridorLength >= _minCorridorLength)
            {
                bestNeighbour = neighbour;
                break;
            }
        }

        return bestNeighbour;
    }

    void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x) //primary is on right of secondary
        {
            _maze[primaryCell.x, primaryCell.y].Visited = true;
            while (primaryCell.x > secondaryCell.x)
            {
                _maze[primaryCell.x, primaryCell.y].LeftWall = false;
                primaryCell.x--;
                _maze[primaryCell.x, primaryCell.y].Visited = true;
            }
        }
        else if (secondaryCell.x > primaryCell.x) //primary is on left of secondary
        {
            _maze[secondaryCell.x, secondaryCell.y].Visited = true;
            while (secondaryCell.x > primaryCell.x)
            {
                _maze[secondaryCell.x, secondaryCell.y].LeftWall = false;
                secondaryCell.x--;
                _maze[secondaryCell.x, secondaryCell.y].Visited = true;
            }
        }
        else if (primaryCell.y < secondaryCell.y) //primary is below secondary
        {
            _maze[primaryCell.x, primaryCell.y].Visited = true;
            while (primaryCell.y < secondaryCell.y)
            {
                _maze[primaryCell.x, primaryCell.y].TopWall = false;
                primaryCell.y++;
                _maze[primaryCell.x, primaryCell.y].Visited = true;
            }
        }
        else if (secondaryCell.y < primaryCell.y) //primary is above secondary
        {
            _maze[secondaryCell.x, secondaryCell.y].Visited = true;
            while (secondaryCell.y < primaryCell.y)
            {
                _maze[secondaryCell.x, secondaryCell.y].TopWall = false;
                secondaryCell.y++;
                _maze[secondaryCell.x, secondaryCell.y].Visited = true;
            }
        }

    }

    void CarvePath(int x, int y)
    {
        if (x < 0 || x > _mazeWidth - 1 || y < 0 || y > _mazeHeight - 1)
        {
            x = y = 0;
            Debug.Log("Invalid start position,, defaulting to 0, 0");
        }

        _currentCell = new Vector2Int(x, y);

        List<Vector2Int> path = new();

        bool deadEnd = false;

        while (!deadEnd)
        {
            Vector2Int nextCell = CheckNeighbours();

            if (nextCell.Equals(_currentCell))
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    _currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();

                    if (!nextCell.Equals(_currentCell)) break;
                }

                if (nextCell.Equals(_currentCell))
                {
                    deadEnd = true;
                }
            }
            else
            {
                BreakWalls(_currentCell, nextCell);
                _currentCell = nextCell;
                path.Add(_currentCell);
            }
        }
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class MazeCell
{

    private bool _visited = false;
    public bool Visited
    {
        get => _visited;
        set => _visited = value;
    }

    private bool _topWall = true;
    public bool TopWall
    {
        get => _topWall;
        set => _topWall = value;
    }

    private bool _leftWall = true;
    public bool LeftWall
    {
        get => _leftWall;
        set => _leftWall = value;
    }

    private bool _floor = true;
    public bool Floor
    {
        get => _floor;
        set => _floor = value;
    }

    public Vector2Int Position { get; }

    public MazeCell(int x, int y)
    {
        Position = new Vector2Int(x, y);
    }
}