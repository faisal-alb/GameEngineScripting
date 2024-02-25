using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHanoiTower : MonoBehaviour
{
    [SerializeField] private GameObject discPrefab;
    [SerializeField] private GameObject pointerPrefab;
    
    [SerializeField] private Transform peg1Transform;
    [SerializeField] private Transform peg2Transform;
    [SerializeField] private Transform peg3Transform;
    
    [SerializeField] private int[] peg1 = { 1, 2, 3, 4 };
    [SerializeField] private int[] peg2 = { 0, 0, 0, 0 };
    [SerializeField] private int[] peg3 = { 0, 0, 0, 0 };

    private GameObject pointer;
    private int currentPeg = 1;

    private void Start()
    {
        float initialWidth = 1.4f;
        float widthIncrement = 0.2f;

        pointer = Instantiate(pointerPrefab, peg1Transform);
        pointer.transform.localPosition = new Vector3(0, -60.0f, 0);
        
        for (int i = 0; i < peg1.Length; i++)
        {
            if (peg1[i] != 0)
            {
                GameObject disc = Instantiate(discPrefab, peg1Transform);

                float width = initialWidth - widthIncrement * (peg1[i] - 1);
                float newPosition = i * (40.0f + 1.0f);

                disc.transform.localScale = new Vector3(width, 1, 1);
                disc.transform.localPosition = new Vector3(0, newPosition, 0);
            }
        }
    }

    public void MoveRight()
    {
        IncrementPeg();
        UpdatePointer();
    }

    public void MoveLeft()
    {
        DecrementPeg();
        UpdatePointer();
    }

    private void UpdatePointer()
    {
        switch (currentPeg)
        {
            case 1:
                pointer.transform.SetParent(peg1Transform);
                pointer.transform.localPosition = new Vector3(0, -60.0f, 0);
                break;
            case 2:
                pointer.transform.SetParent(peg2Transform);
                pointer.transform.localPosition = new Vector3(0, -60.0f, 0);
                break;
            case 3:
                pointer.transform.SetParent(peg3Transform);
                pointer.transform.localPosition = new Vector3(0, -60.0f, 0);
                break;
        }
    }

    private void IncrementPeg()
    {
        if (currentPeg < 3)
        {
            currentPeg++;
        }
        else
        {
            currentPeg = 1;
        }
    }
    
    private void DecrementPeg()
    {
        if (currentPeg > 1)
        {
            currentPeg--;
        }
        else
        {
            currentPeg = 3;
        }
    }

    private Transform GetPegTransform(int pegNumber)
    {
        switch (pegNumber)
        {
            case 1:
                return peg1Transform;
            case 2 :
                return peg2Transform;
            case 3:
                return peg3Transform;
            default:
                return peg1Transform;
        }
    }
}
