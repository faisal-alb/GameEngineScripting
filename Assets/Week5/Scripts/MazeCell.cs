using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject unvisitedBlock;

    private bool isVisited;

    public void VisitBlock()
    {
        isVisited = true;
        
        unvisitedBlock.SetActive(false);
    }

    public void ClearLeftWall()
    {
        leftWall.SetActive(false);
    }
    
    public void ClearRightWall()
    {
        leftWall.SetActive(false);
    }
    
    public void ClearFrontWall()
    {
        leftWall.SetActive(false);
    }
    
    public void ClearBackWall()
    {
        leftWall.SetActive(false);
    }
}
