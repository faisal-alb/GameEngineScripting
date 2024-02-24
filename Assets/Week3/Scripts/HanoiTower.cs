using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    [SerializeField] private GameObject discPrefab;
    
    [SerializeField] private int currentPeg = 1;
    
    [SerializeField] private Transform peg1Transform;
    [SerializeField] private Transform peg2Transform;
    [SerializeField] private Transform peg3Transform;

    [SerializeField] private int[] peg1 = { 1, 2, 3, 4 };
    [SerializeField] private int[] peg2 = { 0, 0, 0, 0 };
    [SerializeField] private int[] peg3 = { 0, 0, 0, 0 };

    private void Start()
    {
        float initWidth = 1.8f;
        float widthIncrement = 0.2f;
        
        for (int i = 0; i < peg1.Length; i++)
        {
            if (peg1[i] != 0)
            {
                int discSize = peg1[i];
                
                GameObject disc = Instantiate(discPrefab, peg1Transform);
                
                float width = initWidth - widthIncrement * (discSize - 1);
                
                disc.transform.localScale = new Vector3(width, 1, 1);
                
                float yPosition = i * (40.0f + 2.0f);

                disc.transform.localPosition = new Vector3(0, yPosition, 0);
            }
        }
    }
    
    [ContextMenu("Move Right")]
    public void MoveRight()
    {
        if(CanMoveRight() == false) return;

        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        if (fromIndex == -1) return;

        int[] toArray = GetPeg(currentPeg + 1);
        int toIndex = GetIndexOfFreeSlot(toArray);

        if (toIndex == -1) return;

        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;
        
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        Transform disc = PopDiscFromCurrentPeg();
        Transform toPeg = GetPegTransform(currentPeg + 1);
        
        disc.SetParent(toPeg);
    }
    
    [ContextMenu("Move Left")]
    public void MoveLeft()
    {
        if(CanMoveLeft() == false) return;
        
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        if (fromIndex == -1) return;

        int[] toArray = GetPeg(currentPeg - 1);
        int toIndex = GetIndexOfFreeSlot(toArray);
        
        if (toIndex == -1) return;

        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;
        
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        Transform disc = PopDiscFromCurrentPeg();
        Transform toPeg = GetPegTransform(currentPeg - 1);
        
        disc.SetParent(toPeg);
    }

    private bool CanMoveRight()
    {
        return currentPeg < 3;
    }
    
    private bool CanMoveLeft()
    {
        return currentPeg > 1;
    }

    private bool CanAddToPeg(int value, int[] peg)
    {
        int topNumberIndex = GetTopNumberIndex(peg);

        if (topNumberIndex == -1) return true;

        int topNumber = peg[topNumberIndex];

        return topNumber > value;
    }

    private int[] GetPeg(int pegNumber)
    {
        if (pegNumber == 1)
        {
            return peg1;
        } else if (pegNumber == 2)
        {
            return peg2;
        }
        else
        {
            return peg3;
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

    public void IncrementPegNumber()
    {
        currentPeg++;
    }
    
    public void DecrementPegNumber()
    {
        currentPeg--;
    }
    
    private Transform GetPegTransform(int pegNumber)
    {
        if (pegNumber == 1)
        {
            return peg1Transform;
        } else if (pegNumber == 2)
        {
            return peg2Transform;
        }
        else
        {
            return peg3Transform;
        }
    }

    private Transform PopDiscFromCurrentPeg()
    {
        Transform currentPegTransform = GetPegTransform(currentPeg);

        int index = currentPegTransform.childCount - 1;

        Transform disk = currentPegTransform.GetChild(index);

        return disk;
    }

    private void MoveNumber(int[] fromArray, int fromIndex, int[] toArray, int toIndex)
    {
        int value = fromArray[fromIndex];

        fromArray[fromIndex] = 0;
        toArray[toIndex] = value;
    }
}
