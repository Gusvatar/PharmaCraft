using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    
    public Sprite spriteBranco;
    public Sprite spriteOriginal;
    public GrabDetecter player;
    private SpriteRenderer spriteRenderer;
    private bool playerNearby = false;
    

    public GameObject ingrediente;



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

        if (other.CompareTag("ItemPegavel"))
        {
            
            if(!player.IsHoldingItem()){
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }else{
                ingrediente = other.transform.gameObject;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)// player sair da area da maquina.
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
        if(player.IsHoldingItem()) // use o método para verificar
        {
            Debug.Log("O jogador está segurando um item!");
        }
        else
        {
            Debug.Log("O jogador não está segurando um item!");
            if(playerNearby)
            {
                if(ingrediente != null) // se houver um item
                {
                    ingrediente.transform.parent = transform;
                }
            }
            
        }
    }
}
