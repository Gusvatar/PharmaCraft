using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balcao : MonoBehaviour
{
    public QueueManager fila;
    private bool playerNearby = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
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
        if(fila.queueSize() > 0){
            fila.RemoverCliente();
        }
    }
}
