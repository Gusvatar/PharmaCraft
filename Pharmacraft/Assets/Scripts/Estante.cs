using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estante : MonoBehaviour
{
    public Sprite spriteBranco;
    public Sprite spriteOriginal;
    private SpriteRenderer spriteRenderer;
    private bool playerNearby = false;
     void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            spriteRenderer.sprite = spriteBranco;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerNearby = false;
            spriteRenderer.sprite = spriteOriginal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
