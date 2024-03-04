using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell mazeCellPrefab;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeHeight;

    private MazeCell[,] mazeGrid;

    private void Start()
    {
        mazeGrid = new MazeCell[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeHeight; z++)
            {
                mazeGrid[x, z] = Instantiate(mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
    /* FINISH LATER

    private IEnumerator GenerateMaze(MazeCell prevCell, MazeCell currentCell)
    {
        currentCell.VisitBlock();
        
        ClearWalls(prevCell, currentCell);
    }

    private IEnumerator<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        Transform currentCellTransform = currentCell.transform;
        Vector3 currentCellPosition = currentCellTransform.position;
        
        int x = (int) currentCellPosition.x;
        int z = (int) currentCellPosition.z;
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
    }
    
    */

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
