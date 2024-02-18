using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int[,] grid = new int[,]
    {
        { 1, 1, 0, 0, 1 },
        { 0, 0, 0, 0, 0},
        { 0, 0, 1, 0, 1},
        { 1, 0, 1, 0, 0},
        { 1, 0, 1, 0, 1}
    };

    private bool[,] hits;

    private int nRows;
    private int nCols;
    private int row;
    private int col;
    private int score;
    private int time;

    [SerializeField] private Transform gridRoot;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject winLabel;
    [SerializeField] private TextMeshProUGUI timeLabel;
    [SerializeField] private TextMeshProUGUI scoreLabel;

    private void Awake()
    {
        nRows = grid.GetLength(0);
        nCols = grid.GetLength(1);
        hits = new bool[nRows, nCols];

        for (int i = 0; i < nRows * nCols; i++)
        {
            Instantiate(cellPrefab, gridRoot);
        }
        
        SelectCurrentCell();
        InvokeRepeating("IncrementTime", 1f, 1f);
    }

    Transform GetCurrentCell()
    {
        int index = (row * nCols) + col;

        return gridRoot.GetChild(index);
    }

    void SelectCurrentCell()
    {
        Transform cell = GetCurrentCell();
        Transform cursor = cell.Find("Cursor");
        cursor.gameObject.SetActive(true);
    }

    void UnselectCurrentCell()
    {
        Transform cell = GetCurrentCell();
        Transform cursor = cell.Find("Cursor");
        cursor.gameObject.SetActive(false);
    }

    public void MoveHorizontal(int amount)
    {
        UnselectCurrentCell();

        col += amount;
        col = Mathf.Clamp(col, 0, nCols - 1);
        
        SelectCurrentCell();
    }
    
    public void MoveVertical(int amount)
    {
        UnselectCurrentCell();

        row += amount;
        row = Mathf.Clamp(row, 0, nRows - 1);
        
        SelectCurrentCell();
    }

    void ShowHit()
    {
        Transform cell = GetCurrentCell();
        Transform hit = cell.Find("Hit");
        hit.gameObject.SetActive(true);
    }
    
    void ShowMiss()
    {
        Transform cell = GetCurrentCell();
        Transform hit = cell.Find("Miss");
        hit.gameObject.SetActive(true);
    }

    void IncrementScore()
    {
        score++;
        scoreLabel.text = string.Format("Score: {0}", score);
    }

    public void Fire()
    {
        if (hits[row, col])
        {
            return;
        }

        hits[row, col] = true;

        if (grid[row, col] == 1)
        {
            ShowHit();
            IncrementScore();
        }
        else
        {
            ShowMiss();
        }
        
        TryEndGame();
    }

    void TryEndGame()
    {
        for (int row = 0; row < nRows; row++)
        {
            for (int col = 0; col < nCols; col++)
            {
                if (grid[row, col] == 0)
                {
                    continue;
                }

                if (hits[row, col] == false)
                {
                    return;
                }
            }
        }
        
        winLabel.SetActive(true);
        CancelInvoke("IncrementTime");
    }

    void IncrementTime()
    {
        time++;

        timeLabel.text = string.Format("{0}:{1}", time / 60, (time % 60).ToString("00"));
    }

    public void Restart()
    {
        UnselectCurrentCell();

        row = 0;
        col = 0;
        
        SelectCurrentCell();

        hits = new bool[nRows, nCols];

        time = 0;
        score = 0;

        timeLabel.text = "0:00";
        scoreLabel.text = "Score: 0";
        
        ResetHitAndMiss();
        RandomizeGrid();
        
        InvokeRepeating("IncrementTime", 1f, 1f);
    }

    void ResetHitAndMiss()
    {
        int numberOfCells = gridRoot.childCount;

        for (int i = 0; i < numberOfCells; i++)
        {
            Transform cell = gridRoot.GetChild(i);
            Transform hit = cell.Find("Hit");
            Transform miss = cell.Find("Miss");
            
            hit.gameObject.SetActive(false);
            miss.gameObject.SetActive(false);
        }
    }

    void RandomizeGrid()
    {
        for (int row = 0; row < nRows; row++)
        {
            for (int col = 0; col < nCols; col++)
            {
                grid[row, col] = UnityEngine.Random.Range(0, 20) > 10 ? 1 : 0;
            }
        }
    }
}
