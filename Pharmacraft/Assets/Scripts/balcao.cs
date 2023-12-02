using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balcao : MonoBehaviour
{
    public GameObject modalEntrega;
    public QueueManager fila;
    

    private bool playerNearby = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            modalEntrega.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("O jogador saiu da área do balcão!");
            // Faça o que for necessário aqui quando o jogador sair do balcão
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
