using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfDetector : MonoBehaviour
{
    public Shelf shelf; // arraste o objeto que tem o script Shelf para este campo no inspetor

    private bool playerNearby = false; // uma variável para verificar se o jogador está perto da prateleira

    void OnTriggerEnter2D(Collider2D other) // quando outro colisor entra na área deste objeto
    {
        if(other.CompareTag("Player")) // se o outro colisor tiver a tag "Player"
        {
            playerNearby = true; // faça playerNearby ser verdadeiro
        }
    }

    void OnTriggerExit2D(Collider2D other) // quando outro colisor sai da área deste objeto
    {
        if(other.CompareTag("Player")) // se o outro colisor tiver a tag "Player"
        {
            playerNearby = false; // faça playerNearby ser falso
        }
    }

    void Update() // a cada frame
    {
        if(playerNearby)
        {
            // Ao pressionar a tecla 1
            if (Input.GetKeyDown(KeyCode.J))
            {
                shelf.SpawnItem1(); // chame o método para spawnar o primeiro item
            }
            // Ao pressionar a tecla 2
            else if (Input.GetKeyDown(KeyCode.K))
            {
                shelf.SpawnItem2(); // chame o método para spawnar o segundo item
            }
            // Ao pressionar a tecla 3
            else if (Input.GetKeyDown(KeyCode.L))
            {
                shelf.SpawnItem3(); // chame o método para spawnar o terceiro item
            }
        }
    }
}
