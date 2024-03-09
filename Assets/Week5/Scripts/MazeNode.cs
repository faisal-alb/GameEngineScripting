using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed
}

public class MazeNode : MonoBehaviour
{
    [SerializeField] private MeshRenderer nodeFloor;
    [SerializeField] private GameObject[] nodeWalls;

    public void RemoveWall(int wallToRemove)
    {
        nodeWalls[wallToRemove].gameObject.SetActive(false);
    }

    public void SetState(NodeState nodeState)
    {
        switch (nodeState)
        {
            case NodeState.Available:
                nodeFloor.material.color = Color.white;
                break;
            case NodeState.Current:
                nodeFloor.material.color = Color.cyan;
                break;
            case NodeState.Completed:
                nodeFloor.material.color = Color.green;
                break;
        }
    }
}
