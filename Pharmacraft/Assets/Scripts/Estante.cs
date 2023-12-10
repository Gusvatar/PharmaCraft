using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estante : MonoBehaviour
{
    public Sprite spriteBranco;
    public Sprite spriteOriginal;
    private SpriteRenderer spriteRenderer;
     
    public GameObject label1;
    public GameObject label2;
    public GameObject label3;
     
     void Start()
    {
        label1 = transform.GetChild(0).gameObject;
        label2 = transform.GetChild(1).gameObject;
        label3 = transform.GetChild(2).gameObject;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = spriteBranco;

            label1.SetActive(true);
            label2.SetActive(true);
            label3.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            spriteRenderer.sprite = spriteOriginal;

            label1.SetActive(false);
            label2.SetActive(false);
            label3.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
