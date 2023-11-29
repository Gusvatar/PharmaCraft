using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeQueueManager : MonoBehaviour
{

    public QueueManager gerenciadorFilaClientes; // Referência ao script de gerenciamento da fila
    public GameObject clientePrefab; // Prefab do cliente
    public float intervaloDeAdicao = 5.0f;


    void Start()
    {
        InvokeRepeating("AdicionarCliente", 0f, intervaloDeAdicao);
    }

    private void AdicionarCliente()
    {
        GameObject novoCliente = Instantiate(clientePrefab, clientePrefab.transform.position, Quaternion.identity);
        gerenciadorFilaClientes.AdicionarCliente(novoCliente);
    }
}
