using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using Unity.Mathematics;

public class Farmer : MonoBehaviour
{
    [SerializeField] private Transform farmHouseTransform;
    [SerializeField] private Transform farmerTransform;
    [SerializeField] private TextMeshPro honeyCounter;
    private int collectedHoney;
    private float farmerCollectionRate;
    private float farmerTimer;
    private bool isSleeping;

    private void Start()
    {
        collectedHoney = 0;
        farmerCollectionRate = 20.0f;
        isSleeping = true;
    }

    private void Update()
    {
        honeyCounter.text = collectedHoney.ToString();
        
        if (isSleeping)
        {
            farmerTimer += Time.deltaTime;

            if (farmerTimer >= farmerCollectionRate)
            {
                isSleeping = false;
                farmerTimer = 0f;
            }
        }
        else
        {
            CheckForHives();
        }
    }

    private void CheckForHives()
    {
        Hive[] beeHives = GameObject.FindObjectsOfType<Hive>();

        if (beeHives.Length == 0)
        {
            isSleeping = true;
            return;
        }

        Hive nextHive = beeHives[Random.Range(1, 2)];
        
        GoToHive(nextHive);
    }

    private void GoToHive(Hive beeHive)
    {
        Vector3 beeHivePosition = beeHive.transform.position;

        float randomDuration = Random.Range(2.8f, 4.2f);

        /*
        farmerTransform.DOMove(beeHivePosition, randomDuration).OnComplete(() =>
        {
            int honeyFromHive = beeHive.CollectHoney();
            
            if (honeyFromHive > 0)
            {
                collectedHoney += honeyFromHive;
            }

            farmerTransform.DOMove(farmHouseTransform.position, randomDuration).OnComplete(() =>
            {
                isSleeping = true;
            });
        }).SetEase(Ease.InOutCubic);
        */
    }
}
