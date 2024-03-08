using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
    [SerializeField] private GameObject _topWall;
    [SerializeField] private GameObject _bottomWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _floor;

    public struct WallState
    {
        public bool Top;
        public bool Bottom;
        public bool Right;
        public bool Left;
        public bool Ground;

        public WallState(bool top, bool bottom, bool right, bool left, bool ground)
        {
            Top = top;
            Bottom = bottom;
            Right = right;
            Left = left;
            Ground = ground;
        }
    }

    public void Init(WallState state)
    {
        _topWall.SetActive(state.Top);
        _bottomWall.SetActive(state.Bottom);
        _rightWall.SetActive(state.Right);
        _leftWall.SetActive(state.Left);
        _floor.SetActive(state.Ground);
    }
}
