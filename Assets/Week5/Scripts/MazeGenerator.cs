using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell mazeCellPrefab;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeHeight;

    private MazeCell[,] mazeGrid;

    private IEnumerator Start()
    {
        mazeGrid = new MazeCell[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeHeight; z++)
            {
                mazeGrid[x, z] = Instantiate(mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        yield return GenerateMaze(null, mazeGrid[0, 0]);
    }

    private IEnumerator GenerateMaze(MazeCell prevCell, MazeCell currentCell)
    {
        currentCell.VisitBlock();
        
        ClearWalls(prevCell, currentCell);

        yield return new WaitForSeconds(0.05f);

        var nextCell = GetNextUnvisitedCell(currentCell);

        if (nextCell != null)
        {
            yield return GenerateMaze(currentCell, nextCell);
        }
    }

    private IEnumerator<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        Transform currentCellTransform = currentCell.transform;
        Vector3 currentCellPosition = currentCellTransform.position;
        
        int x = (int) currentCellPosition.x;
        int z = (int) currentCellPosition.z;

        if ((x + 1) < mazeWidth)
        {
            var rightCell = mazeGrid[x + 1, z];

            if (rightCell.isVisited == false)
            {
                yield return rightCell;
            }
        }
        
        if ((x - 1) >= 0)
        {
            var leftCell = mazeGrid[x - 1, z];

            if (leftCell.isVisited == false)
            {
                yield return leftCell;
            }
        }
        
        if ((z + 1) < mazeHeight)
        {
            var frontCell = mazeGrid[x, z + 1];

            if (frontCell.isVisited == false)
            {
                yield return frontCell;
            }
        }
        
        if ((z - 1) >= 0)
        {
            var backCell = mazeGrid[x, z - 1];

            if (backCell.isVisited == false)
            {
                yield return backCell;
            }
        }
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        List<MazeCell> mazeCellList = new List<MazeCell>();

        while (unvisitedCells.MoveNext())
        {
            mazeCellList.Add(unvisitedCells.Current);
        }

        for (int i = mazeCellList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            (mazeCellList[i], mazeCellList[j]) = (mazeCellList[j], mazeCellList[i]);
        }

        return mazeCellList.FirstOrDefault();
    }
    
    private void ClearWalls(MazeCell prevCell, MazeCell currentCell)
    {
        if (prevCell == null) return;

        if (prevCell.transform.position.x < currentCell.transform.position.x)
        {
            prevCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        
        if (prevCell.transform.position.x > currentCell.transform.position.x)
        {
            prevCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }
        
        if (prevCell.transform.position.z < currentCell.transform.position.z)
        {
            prevCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        
        if (prevCell.transform.position.z > currentCell.transform.position.z)
        {
            prevCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
}
