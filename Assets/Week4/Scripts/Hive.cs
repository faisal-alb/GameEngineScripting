using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hive : MonoBehaviour
{
    [SerializeField] private GameObject beePrefab;
    [SerializeField] private TextMeshPro honeyCounter;
    private float honeyProductionRate;
    private float honeyTimer;
    private int honeyStored;
    private int nectarStored;
    private bool countingDown;

    private void Start()
    {
        // Initializes default values
        honeyProductionRate = 10.0f;
        honeyStored = 0;
        nectarStored = 0;
        countingDown = false;
        
        // Create bees in the Hive
        CreateBees(4);
    }

    private void Update()
    {
        // Updates the Honey Counter Text
        honeyCounter.text = honeyStored.ToString();
        
        // If the Hive has nectar AND it's not creating any honey...
        if (nectarStored > 0 && countingDown == false)
        {
            // ... it resets the timer and starts creating honey
            honeyTimer = honeyProductionRate;
            countingDown = true;
        }

        // If the Hive is creating honey...
        if (countingDown)
        {
            // the timer decreases...
            honeyTimer -= Time.deltaTime;

            // until it reaches 0
            if (honeyTimer <= 0)
            {
                // Convert the nectar into honey
                ProduceHoney();
                
                // if there is still honey in the Hive...
                if (nectarStored > 0)
                {
                    // reset the timer
                    honeyTimer = honeyProductionRate;
                }
                else
                {
                    // Tell the Hive to not try to create any honey
                    countingDown = false;
                }
            }
        }
    }

    // This method adds nectar to the Hive and starts the countdown to producing honey
    public void GiveNectar()
    {
        nectarStored++;

        if (countingDown == false && nectarStored > 0)
        {
            honeyTimer = honeyProductionRate;
            countingDown = true;
        }
    }

    // This method returns the honey stored and resets the Hive's honey to 0
    public int CollectHoney()
    {
        var prevHoneyStored = honeyStored;
        
        if (honeyStored > 0)
        {
            honeyStored = 0;
            
            return prevHoneyStored;
        }

        return honeyStored;
    }

    // This method creates bees around the Hive
    private void CreateBees(int numberOfBees)
    {
        for (int i = 0; i < numberOfBees; i++)
        {
            // Get a random offset around the hive in a circle of radius 1.4
            Vector3 randomOffset = Random.insideUnitCircle * 1.4f;
            // Get the hives position
            Vector3 hivePosition = transform.position;
            // Combine the random offset with the hives position to get new positions around the hive (While maintaining a constant z vector of -1.0f
            Vector3 newPosition = new Vector3(hivePosition.x + randomOffset.x, hivePosition.y + randomOffset.y, -1.0f);
            
            // Create the Bee
            GameObject bee = Instantiate(beePrefab, newPosition, Quaternion.identity);

            // Get the Bee's component
            Bee beeComponent = bee.GetComponent<Bee>();
            
            // Tell the Bee which Hive belongs to it
            beeComponent.InitializeHive(this);
        }
    }

    // This method simply removes 1 nectar from the hive and converts it to 1 honey
    private void ProduceHoney()
    {
        if (nectarStored > 0)
        {
            nectarStored--;
            honeyStored++;
        }
    }
}
