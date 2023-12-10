using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public GameObject[] items; // arraste os prefabs dos itens para este array no inspetor
    public Transform spawnPoint; // arraste um objeto vazio para este campo no inspetor, ele será o ponto onde os itens serão instanciados

    // Método para spawnar o primeiro item
    public void SpawnItem1()
    {
        GameObject item = Instantiate(items[0], spawnPoint.position - new Vector3(0.5f, 0 ,0 ), Quaternion.identity);
    }

    // Método para spawnar o segundo item
    public void SpawnItem2()
    {
        GameObject item = Instantiate(items[1], spawnPoint.position, Quaternion.identity);
    }

    // Método para spawnar o terceiro item
    public void SpawnItem3()
    {
        GameObject item = Instantiate(items[2], spawnPoint.position + new Vector3(0.5f, 0 ,0 ), Quaternion.identity);
    }
}
