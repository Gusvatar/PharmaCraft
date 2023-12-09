using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool processada = false;
    public Sprite processedSprite;
    public int value = 1;

    public GameObject label;

    void Start()
    {
        
        label = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            label.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            label.SetActive(false);
        }
    }
}
