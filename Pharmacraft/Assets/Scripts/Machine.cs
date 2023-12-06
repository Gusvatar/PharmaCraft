using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    
    public Sprite spriteBranco;
    public Sprite spriteOriginal;
    public GrabDetecter  player;
    private SpriteRenderer spriteRenderer;
    private bool playerNearby = false;
    

     void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // pegue a referência para o SpriteRenderer
    }

    void OnTriggerEnter2D(Collider2D other)// player entrar na area da maquina.
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            spriteRenderer.sprite = spriteBranco;
        }
    }
    void OnTriggerExit2D(Collider2D other)// player sair da area da maquina.
{
    if (other.CompareTag("Player")) 
    {
        playerNearby = false;
        spriteRenderer.sprite = spriteOriginal;
        Debug.Log("O jogador saiu da área da maquina!");
    }
}

    
    // Update is called once per frame
    void Update()
    {
        if(player.IsHoldingItem()) // use o método para verificar
        {
            Debug.Log("O jogador está segurando um item!");
        }
        else
        {
            Debug.Log("O jogador não está segurando um item!");
            if(playerNearby) // se o jogador estiver perto
            {
                GameObject item = player.GetHeldItem(); // pegue o item que o jogador estava segurando
                if(item != null) // se houver um item
                {
                    Debug.Log("CHEGUEI AKI");
                    item.transform.parent = this.transform; // faça o item ser filho da máquina
                    item.SetActive(false); // desative o item
                }
            }
            
        }
    }
}
