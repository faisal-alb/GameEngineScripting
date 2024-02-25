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

    [SerializeField] private GameObject winInfo;

    private GameObject pointer;
    private Transform currentDisc;
    
    private int[] fromArray;
    private int fromIndex;
    private int currentPeg = 1;
    
    private bool isDiscUp = false;

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
        
        winInfo.gameObject.SetActive(false);
    }

    public void MoveRight()
    {
        IncrementPeg();
        UpdatePointer();

        if (isDiscUp)
        {
            Transform currentPegTransform = GetPegTransform(currentPeg);
        
            currentDisc.SetParent(currentPegTransform);
            currentDisc.localPosition = new Vector3(0, 480.0f, 0);
        }
    }

    public void MoveLeft()
    {
        DecrementPeg();
        UpdatePointer();
        
        if (isDiscUp)
        {
            Transform currentPegTransform = GetPegTransform(currentPeg);
        
            currentDisc.SetParent(currentPegTransform);
            currentDisc.localPosition = new Vector3(0, 480.0f, 0);
        }
    }

    public void MoveUp()
    {
        if (isDiscUp) return;
        
        fromArray = GetPeg(currentPeg);
        fromIndex = GetTopNumberIndex(fromArray);
        
        currentDisc = PopDiscFromCurrentPeg();

        currentDisc.transform.localPosition = new Vector3(0, 480.0f, 0);

        isDiscUp = true;
    }

    // FIXED : Minor Bug, can't move disc back down to original peg because it's still in the same place in the array.
    public void MoveDown()
    {
        if (currentDisc != null)
        {
            int[] toArray = GetPeg(currentPeg);
            int toIndex = GetIndexOfFreeSlot(toArray);
            
            if (fromArray == toArray)
            {
                toIndex = fromIndex;
            }
            
            if (toIndex == -1) return;

            if (fromArray == toArray && fromIndex == toIndex)
            {
                float samePosition = (toArray.Length - toIndex - 1) * (40.0f + 1.0f);
                currentDisc.localPosition = new Vector3(0, samePosition, 0);
                
                currentDisc = null;
                isDiscUp = false;
                
                return;
            }
            
            if (!CanAddToPeg(fromArray[fromIndex], toArray)) return;
            
            Transform currentPegTransform = GetPegTransform(currentPeg);

            float newPosition = (toArray.Length - toIndex - 1) * (40.0f + 1.0f);

            currentDisc.SetParent(currentPegTransform);
            currentDisc.localPosition = new Vector3(0, newPosition, 0);
                
            MoveNumber(fromArray, fromIndex, toArray, toIndex);
                
            currentDisc = null;
            isDiscUp = false;

            if (currentPeg != 3) return;

            for (int i = 0; i < peg3.Length; i++)
            {
                if (peg3[i] == 0)
                {
                    return;
                }
            }
            
            winInfo.gameObject.SetActive(true);
        }
    }

    // Couldn't implement yet ):
    public void RestartGame()
    {
        winInfo.gameObject.SetActive(false);
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

    private Transform PopDiscFromCurrentPeg()
    {
        Transform currentPegTransform = GetPegTransform(currentPeg);

        for (int i = currentPegTransform.childCount - 1; i >= 0; i--)
        {
            Transform disc = currentPegTransform.GetChild(i);
            
            if (disc.CompareTag("Disc")) return disc;
        }
        
        return null;
    }

    private void MoveNumber(int[] fromArray, int fromIndex, int[] toArray, int toIndex)
    {
        int value = fromArray[fromIndex];

        fromArray[fromIndex] = 0;
        toArray[toIndex] = value;
    }
    
    private int[] GetPeg(int pegNumber)
    {
        switch (pegNumber)
        {
            case 1:
                return peg1;
            case 2 :
                return peg2;
            case 3:
                return peg3;
            default:
                return peg1;
        }
    }
    
    private int GetTopNumberIndex(int[] peg)
    {
        for (int i = 0; i < peg.Length; i++)
        {
            if (peg[i] != 0) return i;
        }
        
        return -1;
    }

    private int GetIndexOfFreeSlot(int[] peg)
    {
        for (int i = peg.Length - 1; i >= 0; i--)
        {
            if (peg[i] == 0) return i;
        }
        
        return -1;
    }

    private bool CanAddToPeg(int value, int[] peg)
    {
        int topNumberIndex = GetTopNumberIndex(peg);

        if (topNumberIndex == -1) return true;

        int topNumber = peg[topNumberIndex];

        return topNumber > value;
    }
}
