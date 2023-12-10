using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerMachine : MonoBehaviour
{
    public Sprite spriteBranco;
    public Sprite spriteOriginal;

    public GrabDetecter player;
    private SpriteRenderer spriteRenderer;
    private bool playerNearby = false;
    private bool isProcessionItem = false;

    public int[] recepy = new int[3];
    private int recepyIndex = 0;
    public GameObject remedyPrefab; 

    
    

    public GameObject ingrediente;
    private Vector3 itemStartPosition = new Vector3(0,0,0);
    private float processTime = 8f;

    public AudioSource Audio;

    public AudioClip ProcessamentoMaquina;

    public AudioClip TerminouMixagem;



    public void addToRecepy(GameObject item){
        recepy[recepyIndex++] = item.GetComponent<Item>().value;
        if(recepyIndex==2) isProcessionItem = true;
    }

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

        if (other.CompareTag("ItemPegavel"))
        {
            bool processed =  other.transform.gameObject.GetComponent<Item>().processada;
            if(!other.transform.gameObject.GetComponent<Recepy>() && processed)
                ingrediente = other.transform.gameObject;
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

    void Update()
    {
        if(!player.IsHoldingItem() && recepyIndex != 3) 
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

                    addToRecepy(ingrediente);
                    Destroy(ingrediente);
                    ingrediente = null;
                }
            }
            
        }

        if(isProcessionItem){
            processTime -= Time.deltaTime;
        }

        if(recepyIndex == 3 && processTime <= 0){
            Audio.clip = TerminouMixagem;
            Audio.Play();
            GameObject Remedio = Instantiate(remedyPrefab, remedyPrefab.transform.position, Quaternion.identity);
            Remedio.GetComponent<Recepy>().recepy = recepy;

            Debug.Log("Remedio Feito");
            Remedio.GetComponent<Item>().processada = true;
            Remedio.transform.position = itemStartPosition;
            Remedio.transform.parent = null;
            Remedio.GetComponent<Rigidbody2D>().isKinematic = false;


            processTime = 8f;
            ingrediente = null;
            isProcessionItem = false;

            recepyIndex = 0;
        }
    }
}
