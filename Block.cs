using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] int drop_chance;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;
    public GameObject Powerup;

    //state variables
    [SerializeField] int timesHit; //TODO only serialized for debug purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    

    // when ball hits brick...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // break brick and play sound/particle explosion
        if (tag == "Breakable")
        {
            HandleHit();
        }
        
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array. OBJECT:" + gameObject.name);
        }
        
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
            PowerupDrop();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void PowerupDrop()
    {
        // randomly drop powerup
        System.Random random = new System.Random();
        int roll = random.Next(1, 101);

        // UnityEngine.Debug.Log(roll);

        if (roll < drop_chance)
        {
            Instantiate_Powerup();
        }
        else
        {
            // UnityEngine.Debug.Log("no drop loser");
        }
    }

    private void DestroyBlock()
    {
        
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
        PlayBlockDestroySFX();
        
        
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    public void Instantiate_Powerup()
    {
        var go = Instantiate(Powerup, gameObject.transform.position, gameObject.transform.rotation);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);

        Destroy(sparkles, 2f);
    }
}
