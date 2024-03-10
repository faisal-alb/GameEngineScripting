using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// Credits : https://www.youtube.com/watch?v=OutlTTOm17M

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private bool generateInstantly;
    [SerializeField] private MazeNode nodePrefab;
    [SerializeField] private Vector2Int mazeSize;
    private MazeNode firstNode;
    private MazeNode lastNode;

    private void Start()
    {
        if (generateInstantly)
        {
            GenerateMazeInstant(mazeSize);
            SpawnMazeComponents();
        }
        else
        {
            StartCoroutine(GenerateMaze(mazeSize));
            SpawnMazeComponents();
        }
    }

    private IEnumerator GenerateMaze(Vector2Int size)
    {
        List<MazeNode> mazeNodes = new List<MazeNode>();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePosition = new Vector3(x - (size.x / 2.0f), 0, y - (size.y / 2.0f));
                MazeNode newNode = Instantiate(nodePrefab, nodePosition, Quaternion.identity, transform);
                
                mazeNodes.Add(newNode);

                if (x == 0 && y == (size.y - 1))
                {
                    firstNode = newNode;
                }

                if (x == (size.x - 1) && y == 0)
                {
                    lastNode = newNode;
                }
                
                yield return null;
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();
        
        currentPath.Add(mazeNodes[Random.Range(0, mazeNodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < mazeNodes.Count)
        {
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = mazeNodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            // Checks the node to the right of the current node.
            if (currentNodeX < size.x - 1)
            {
                if (!completedNodes.Contains(mazeNodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(mazeNodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            // Checks the node to the left of the current node.
            if (currentNodeX > 0)
            {
                if (!completedNodes.Contains(mazeNodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(mazeNodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            // Checks the node above the current node.
            if (currentNodeY < size.y - 1)
            {
                if (!completedNodes.Contains(mazeNodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(mazeNodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            // Checks the node below the current node.
            if (currentNodeY > 0)
            {
                if (!completedNodes.Contains(mazeNodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(mazeNodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }
            
            // Picks the next node.
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = mazeNodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }
                
                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);
                
                currentPath[currentPath.Count -1].SetState(NodeState.Completed);
                
                currentPath.RemoveAt(currentPath.Count - 1);
            }
            
            firstNode.SetState(NodeState.Available);
            lastNode.SetState(NodeState.Available);

            yield return new WaitForSeconds(0.05f);
        }
    }
    
    private void GenerateMazeInstant(Vector2Int size)
    {
        List<MazeNode> mazeNodes = new List<MazeNode>();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePosition = new Vector3(x - (size.x / 2.0f), 0, y - (size.y / 2.0f));
                MazeNode newNode = Instantiate(nodePrefab, nodePosition, Quaternion.identity, transform);
                
                mazeNodes.Add(newNode);
                
                if (x == 0 && y == (size.y - 1))
                {
                    firstNode = newNode;
                }

                if (x == (size.x - 1) && y == 0)
                {
                    lastNode = newNode;
                }
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();
        
        currentPath.Add(mazeNodes[Random.Range(0, mazeNodes.Count)]);

        while (completedNodes.Count < mazeNodes.Count)
        {
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = mazeNodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            // Checks the node to the right of the current node.
            if (currentNodeX < size.x - 1)
            {
                if (!completedNodes.Contains(mazeNodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(mazeNodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            // Checks the node to the left of the current node.
            if (currentNodeX > 0)
            {
                if (!completedNodes.Contains(mazeNodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(mazeNodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            // Checks the node above the current node.
            if (currentNodeY < size.y - 1)
            {
                if (!completedNodes.Contains(mazeNodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(mazeNodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            // Checks the node below the current node.
            if (currentNodeY > 0)
            {
                if (!completedNodes.Contains(mazeNodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(mazeNodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }
            
            // Picks the next node.
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = mazeNodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }
                
                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);
                
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }

    private void SpawnMazeComponents()
    {
        Debug.Log("test1");
        
        if (playerPrefab != null && firstNode != null)
        {
            Debug.Log("test2");
            Instantiate(playerPrefab, firstNode.transform.position, Quaternion.identity);
        }
    }
}
