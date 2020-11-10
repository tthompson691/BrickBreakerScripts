using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Powerup : MonoBehaviour

{    // fall speed must be negative
    [SerializeField] float fall_speed;

    //cached references
    GameObject Ball;

    int powerupRoll;

    // Start is called before the first frame update
    void Start()
    {
        // choose powerup type and associated sprite upon instantiation
        // roll random number between 1 and 1000, index associated powerup

        powerupRoll = Random.Range(0, 1000);
        UnityEngine.Debug.Log("Roll: " + powerupRoll);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        falldown();
    }

    // this makes the power up fall down the screen
    private void falldown()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, fall_speed);
        
    }

    // destroy the powerup if it contacts either paddle or lose collider
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Paddle")
        {
            //apply powerup effect to game
            if (powerupRoll > 0 && powerupRoll < 1000)
            {
                duplicateBall();
            }

            Destroy(gameObject);
        }
        
        
        // Destroy(gameObject);
    }

    private void duplicateBall()
    {
        Ball ball = FindObjectOfType<Ball>();

        Instantiate(ball, ball.transform.position, ball.transform.rotation);
    }
}
