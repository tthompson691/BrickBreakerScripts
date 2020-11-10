using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // parameters
    [SerializeField] int breakableBlocks; // serialized for debugging purposes
    [SerializeField] public int balls = 0;
    public bool hasStarted = false;
    

    // cached reference
    SceneLoader sceneloader;
    Level level;

    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--; //decrease counter by 1

        if (breakableBlocks <= 0)
        {
            sceneloader.LoadNextScene();
        }
    }

    public void countBalls()
    {
        balls++;
    }

    public void loseBalls()
    {
        balls--;
    }
    public void StartOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {

            hasStarted = true;
        }

    }
}
