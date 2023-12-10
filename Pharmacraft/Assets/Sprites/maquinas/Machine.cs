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
    private bool isProcessionItem = false;


    public int value = 0;
    
    
    

    public GameObject ingrediente;
    private Vector3 itemStartPosition = new Vector3(0,0,0);
    private float processTime = 8f;

    public AudioSource Audio;

    public AudioClip ProcessamentoMaquina;

    public AudioClip ItemProcessado;


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

        if (other.CompareTag("ItemPegavel") && !ingrediente)
        {
            if(!other.transform.gameObject.GetComponent<Item>().processada && !isProcessionItem){
                ingrediente = other.transform.gameObject;

                if(ingrediente.GetComponent<Item>().value != value){
                    ingrediente = null;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerNearby = false;
            spriteRenderer.sprite = spriteOriginal;
        }

        if (other.CompareTag("ItemPegavel") && !isProcessionItem)
        {
            ingrediente = null; 
        }
    }

    void Update()
    {
        if(!player.IsHoldingItem() && !isProcessionItem) 
        {
            if(playerNearby)
            {
                if(ingrediente != null) 
                {
                    Audio.clip = ProcessamentoMaquina;
                    Audio.Play();

                    isProcessionItem = true;
                    itemStartPosition = ingrediente.transform.position;
                    Debug.Log(itemStartPosition);

                    ingrediente.transform.position = transform.position;
                    ingrediente.transform.parent = transform;
                }
            }
            
        }

        if(ingrediente){
            processTime -= Time.deltaTime;
        }

        if(ingrediente && processTime <= 0 && !ingrediente.GetComponent<Item>().processada){
            Audio.clip = ItemProcessado;
            Audio.Play();

            Debug.Log("Remedio Feito");
            ingrediente.GetComponent<Item>().processada = true;
            ingrediente.transform.position = itemStartPosition;
            ingrediente.transform.parent = null;
            ingrediente.GetComponent<Rigidbody2D>().isKinematic = false;
            ingrediente.GetComponent<SpriteRenderer>().sprite = ingrediente.GetComponent<Item>().processedSprite;

            processTime = 8f;
            ingrediente = null;
            isProcessionItem = false;
        }
    }
}
