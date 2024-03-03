using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class Bee : MonoBehaviour
{
    private Hive beeHive;

    private void Start()
    {
        CheckForFlowers();
    }

    // Initialize the bee's main hive
    public void InitializeHive(Hive hive)
    {
        beeHive = hive;
    }

    
    private void CheckForFlowers()
    {
        // Find all the flowers that exist in the game
        Flower[] flowers = GameObject.FindObjectsOfType<Flower>();

        // If a flower exist...
        if (flowers.Length != 0)
        {
            // ... the bee randomly picks one and goes to it
            Flower targetFlower = flowers[Random.Range(0, flowers.Length)];
            
            GoToFlower(targetFlower);
        }
    }

    private void GoToFlower(Flower targetFlower)
    {
        Vector3 targetFlowerPosition = targetFlower.transform.position;
        Vector3 randomOffset = Random.insideUnitCircle * 0.2f;
        float randomDuration = Random.Range(2.8f, 4.2f);
        
        transform.DOMove(new Vector3(targetFlowerPosition.x + randomOffset.x, targetFlowerPosition.y + randomOffset.y, -1.0f), randomDuration).OnComplete(() =>
        {
            if (targetFlower.TakeNectar())
            {
                GoToHive();
            }
            else
            {
                CheckForFlowers();
            }
        }).SetEase(Ease.InOutCubic);
    }

    private void GoToHive()
    {
        float randomDuration = Random.Range(2.2f, 3.4f);

        transform.DOMove(beeHive.transform.position, randomDuration).OnComplete(() =>
        {
            beeHive.GiveNectar();
            CheckForFlowers();
        }).SetEase(Ease.InOutCubic);
    }
}
