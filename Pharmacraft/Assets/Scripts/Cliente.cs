using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
/*     public float velocidadeMovimento = 5f; // Velocidade de movimento do cliente
    public float tempoNaPorta = 2f; // Tempo que o cliente fica na porta antes de sair
    private bool indoParaPorta = false;

     public Vector3 alvoPosicao; // A posição alvo para onde o cliente se move
    public float velocidade = 2.0f; // Velocidade de movimento
    public float distânciaParadaY = 0.1f; // Distância mínima no eixo Y para considerar que o cliente chegou ao alvo

    private bool estaMovendo = true;

    private void Update()
    {
        if (estaMovendo)
        {
            // Calcula a distância no eixo Y entre o cliente e o alvo
            float distânciaY = Mathf.Abs(transform.position.y - alvoPosicao.y);

            // Verifica a distância no eixo Y para o alvo
            if (distânciaY <= distânciaParadaY)
            {
                estaMovendo = false;
                return;
            }

            // Calcula a direção do movimento
            Vector3 direção = alvoPosicao - transform.position;
            direção.Normalize();

            // Move o cliente em direção à posição alvo
            transform.Translate(direção * velocidade * Time.deltaTime);
        }
    }


    void Start()
    {
        // Inicia o movimento do cliente na fila
        MoverNaFila();
    }


    // Método para destruir o objeto do cliente após um tempo
    private void DestruirCliente()
    {
        Destroy(gameObject);
    }

    // Método para iniciar o movimento do cliente na fila
    private void MoverNaFila()
    {
        // Implemente a lógica de movimentação na fila aqui
    }

    // Método chamado quando o cliente chega à porta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Porta"))
        {
            // Define o cliente para ser destruído após um tempo na porta
            Invoke("DestruirCliente", tempoNaPorta);
        }
    }  */


    public GameObject alvoPosicao;
    public bool atendido = false;
    public float velocidade = 2.0f;
    public float distânciaParadaY = 1f;
    public bool prioridade = false;



    private void Update()
    {
        if(!alvoPosicao)
            return;
        // Calcula a direção do movimento
        Vector3 direção = alvoPosicao.transform.position - transform.position;

        // Verifica a distância para o alvo
        if (direção.magnitude <= distânciaParadaY)
        {
            velocidade = 0;
            return;
        }else if (!atendido){
            velocidade = 2.0f;
        }

        direção.Normalize();

        // Move o cliente em direção à posição alvo
        transform.Translate(direção * velocidade * Time.deltaTime);
        
    }
}
