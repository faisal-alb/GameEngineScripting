using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Restarting Scene Credits:
// https://stackoverflow.com/questions/65851443/how-do-i-restart-the-scene-that-im-currently-in-through-script-in-unity-2d-so

public class Restart : MonoBehaviour
{
    private float rotationSpeed = 20.0f;
    
    private void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RestartScene();
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
