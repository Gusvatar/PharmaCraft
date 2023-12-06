using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balcao : MonoBehaviour
{
    public QueueManager fila;
    private bool playerNearby = false;
    private int money = 0;
    public TMPro.TextMeshPro scoreText;


    void updateCurrency(int value)
    {
        money += value;
        scoreText.text = money.ToString();
    }


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
            updateCurrency(10);
        }
    }

    void EntregarRemédio()
    {
        if(fila.queueSize() > 0){
            fila.top().GetComponent<Cliente>().sucesso = true;
            fila.RemoverCliente();
        }
    }
}
