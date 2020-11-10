using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    Level level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        level = FindObjectOfType<Level>();
        
        if (collision.gameObject.tag == "Powerup")
        {
            return;
        }
        else
        {
            // only end game if there are zero balls on screen.
            // if there are multiple balls, and one collides, destroy it
            // and keep game going
            level.loseBalls();
            int remainingBalls = level.balls;

            if (remainingBalls > 0)
            {
                UnityEngine.Debug.Log("Remining balls:" + remainingBalls);
                Destroy(collision.gameObject);
            }
            else
            {
                UnityEngine.Debug.Log("Remining balls:" + remainingBalls);
                SceneManager.LoadScene("Game Over");
            }

        } 

    }
}
