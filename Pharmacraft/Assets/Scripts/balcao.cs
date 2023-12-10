using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class balcao : MonoBehaviour
{
    public QueueManager fila;
    private bool playerNearby = false;
    public TMPro.TextMeshPro scoreText;
    public Player player;

    public GameObject remedio;
    
    public GameObject label;

    public AudioSource Audio;

    public AudioClip RemedioErrado;

    public AudioClip RemedioCorreto;




    private void Start()
    {
        label = transform.GetChild(0).gameObject;
    }

    void updateCurrency(int value)
    {
        fila.money += value;
        scoreText.text = fila.money.ToString();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            label.SetActive(true);

            remedio = other.transform.gameObject.GetComponent<GrabDetecter>().GetHeldItem();
            if(remedio.GetComponent<Recepy>()){
                playerNearby = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            remedio = null;
            
            label.SetActive(false);
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            EntregarRemédio();
        }
    }

    void EntregarRemédio()
    {
        if(fila.queueSize() > 0 && remedio){
            remedio.transform.parent = fila.top().transform;
            int value = fila.top().GetComponent<Cliente>().validRecepy(remedio.GetComponent<Recepy>().recepy);

            if(value <= 10){
                Audio.clip = RemedioErrado;
                Audio.Play();
            }else{
                Audio.clip = RemedioCorreto;
                Audio.Play();
            }


            fila.top().GetComponent<Cliente>().sucesso = true;
            fila.RemoverCliente();

            Destroy(remedio);
            remedio = null;
            updateCurrency(value);
        }
    }
}
