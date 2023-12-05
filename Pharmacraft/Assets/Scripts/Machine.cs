using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    private bool playerNearby = false;
    public Sprite spriteBranco;
    public Sprite spriteOriginal;
    private SpriteRenderer spriteRenderer;

     void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // pegue a referência para o SpriteRenderer
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
    if (other.CompareTag("Player")) // remova o ponto e vírgula aqui
    {
        playerNearby = false;
        spriteRenderer.sprite = spriteOriginal;
        Debug.Log("O jogador saiu da área da maquina!");
    }
}

    
    // Update is called once per frame
    void Update()
    {
        if(playerNearby){
            Debug.Log("O PLAYER ESTA NA MAQUINA 1");
        }
    }
}
