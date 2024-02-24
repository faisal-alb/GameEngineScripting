using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public GameObject[] SpawnLocations;

    public void SelectColumn(int Column)
    {
    }

    void SpawnPiece(int Column)
    {
        Instantiate(PlayerOne, SpawnLocations[Column].transform.position, Quaternion.identity);
    }
}
