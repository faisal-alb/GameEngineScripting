using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private SpriteRenderer flowerRenderer;
    private float nectarProductionRate;
    private float nectarTimer;
    private bool hasNectar;

    private void Start()
    {
        // Gets the Flower Sprite
        flowerRenderer = GetComponent<SpriteRenderer>();

        // Sets the Initial Nectar Production Rate (Time it takes to make 1 Nectar)
        nectarProductionRate = 5.0f;
    }

    private void Update()
    {
        // If the flower doesn't have nectar...
        if (hasNectar == false)
        {
            // ... the timer starts for the flower to create the nectar
            nectarTimer += Time.deltaTime;

            // The timer reaches the time needed to create nectar
            if (nectarTimer >= nectarProductionRate)
            {
                // Flower now has nectar
                hasNectar = true;

                // The flower's color is updated to match its nectar status
                UpdateFlowerColor();
                
                // The nectar timer is reset
                nectarTimer = 0.0f;
            }
        }
    }

    // This method takes the nectar from the flower and returns a true/false value dependent on whether the flower had nectar to begin with
    public bool TakeNectar()
    {
        if (hasNectar)
        {
            hasNectar = false;
            
            UpdateFlowerColor();
            
            return true;
        }
        
        return false;
    }

    // This method updates the color of the flower dependent on if it has nectar in it 
    private void UpdateFlowerColor()
    {
        flowerRenderer.color = hasNectar ? Color.white : Color.gray;
    }
}
