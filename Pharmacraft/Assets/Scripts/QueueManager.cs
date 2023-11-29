using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour{

    public Sprite prioritySprite;
    public Sprite[] clientSprites;
    private Queue<GameObject> filaDeClientes = new Queue<GameObject>();
    private Queue<GameObject> filaDePrioridade = new Queue<GameObject>();

    public int maxQueue = 5;
    public GameObject balcao;
    public GameObject balcaoPrioridade;
    private bool clientePrioridadeEmFila = false;


    public int queueSize(){
        return filaDeClientes.Count;
    }


    public void AdicionarCliente(GameObject novoCliente) {
        if (filaDeClientes.Count >= maxQueue)
            return;
            
        Sprite randomSprite;

        int rand = Random.Range(0, clientSprites.Length+1);
        if(rand < clientSprites.Length || clientePrioridadeEmFila){
            randomSprite = clientSprites[rand];
        } else{
            clientePrioridadeEmFila = true;
            randomSprite = prioritySprite;
        }
        novoCliente.GetComponent<SpriteRenderer>().sprite = randomSprite;


        bool isPriority = novoCliente.GetComponent<SpriteRenderer>().sprite == prioritySprite;
        if(isPriority){
            filaDePrioridade.Enqueue(novoCliente);
        }else{
            novoCliente.transform.position = novoCliente.transform.position + new Vector3(1f, 0, 0);
            filaDeClientes.Enqueue(novoCliente);
        }

        
        if (filaDeClientes.Count == 1) {
            novoCliente.GetComponent<Cliente>().alvoPosicao = balcao;
        }
        else if (isPriority){
            novoCliente.GetComponent<Cliente>().prioridade = true;
            novoCliente.GetComponent<Cliente>().alvoPosicao = balcaoPrioridade;
        }
        else {
            GameObject[] filaArray = filaDeClientes.ToArray();
            GameObject ultimoCliente = filaArray[filaArray.Length - 2];
            novoCliente.GetComponent<Cliente>().alvoPosicao = ultimoCliente;
        }

        
    }

    public void AtualizarFila() {
        filaDeClientes.Peek().GetComponent<Cliente>().alvoPosicao = balcao;
        filaDeClientes.Peek().GetComponent<Cliente>().velocidade = 2.0f;
    }

    public void RemoverCliente() {
        GameObject[] filaArray = filaDeClientes.ToArray();
        
        if (filaDeClientes.Count > 0)
        {
            GameObject clienteRemovido = filaDeClientes.Dequeue();
            clienteRemovido.GetComponent<Cliente>().atendido = true;
            StartCoroutine(AnimacaoRemocaoCliente(clienteRemovido));
            AtualizarFila();
        }
    }

    public void RemoverClientePrioridade() {
        GameObject[] filaArray = filaDePrioridade.ToArray();
        
        
        if (filaDeClientes.Count > 0)
        {
            GameObject clienteRemovido = filaDePrioridade.Dequeue();
            clienteRemovido.GetComponent<Cliente>().atendido = true;
            StartCoroutine(AnimacaoRemocaoCliente(clienteRemovido));
            AtualizarFila();
        }
    }
    private IEnumerator AnimacaoRemocaoCliente(GameObject cliente) {
        float duracaoAnimacaoX = 1.0f; // Duração da animação lateral 
        float duracaoAnimacaoY = 5.0f; // Duração da animação horizontal
        float distanciaMovimentoLateral = -1.0f; // Distância que o cliente se moverá lateralmente


        Vector3 posicaoInicial = cliente.transform.position;
        Vector3 posicaoFinal = posicaoInicial + new Vector3(distanciaMovimentoLateral, 0, 0);

        float tempoInicial = Time.time;

        while (Time.time < tempoInicial + duracaoAnimacaoX)
        {
            float progresso = (Time.time - tempoInicial) / duracaoAnimacaoX;
            cliente.transform.position = Vector3.Lerp(posicaoInicial, posicaoFinal, progresso);
            yield return null;
        }

        // Aguarde um curto período para exibir o cliente na nova posição
        yield return new WaitForSeconds(0.5f);

        // Inverta a direção da animação (volte para a posição inicial)
        posicaoInicial = cliente.transform.position;
        posicaoFinal = cliente.transform.position - new Vector3(0, 5, 0);
        tempoInicial = Time.time;

        while (Time.time < tempoInicial + duracaoAnimacaoY) {
            float progresso = (Time.time - tempoInicial) / duracaoAnimacaoY;
            cliente.transform.position = Vector3.Lerp(posicaoInicial, posicaoFinal, progresso);
            yield return null;
        }

        // Aguarde um curto período para exibir o cliente na posição final
        
        yield return new WaitForSeconds(duracaoAnimacaoY);

        // Destrua o cliente após a animação
        if(cliente.GetComponent<Cliente>().prioridade) clientePrioridadeEmFila = false;
        Destroy(cliente);
    }
}
