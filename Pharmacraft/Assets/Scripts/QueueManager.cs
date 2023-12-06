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
    public float tempoParaSerAtendido = 60.0f;

    public int money = 0;

    void Update()
    {
        bool atendido = top().GetComponent<Cliente>().atendido;
        if(queueSize() > 0 && !atendido){

            if(tempoParaSerAtendido <= 30 && tempoParaSerAtendido >= 25){
                top().GetComponent<Cliente>().showMediumBalloon();
            }else if(tempoParaSerAtendido <= 15 && tempoParaSerAtendido >= 10){
                top().GetComponent<Cliente>().showAngryTimeBalloon();
            }else if(tempoParaSerAtendido <= 25){
                top().GetComponent<Cliente>().removeMediumBalloon();
                top().GetComponent<Cliente>().removeAngryTimeBalloon();
            }
        }
        

        tempoParaSerAtendido -= Time.deltaTime;
        //Debug.Log(tempoParaSerAtendido);
        if (tempoParaSerAtendido <= 0)
        {
            Debug.Log("Cliente saiu da fila por ter excedido o tempo.");
            RemoverCliente();
        }
        
    }
    public int queueSize(){
        return filaDeClientes.Count;
    }

     public int priorityQueueSize(){
        return filaDePrioridade.Count;
    }

    public GameObject top(){
        GameObject[] filaArray = filaDeClientes.ToArray();
        return filaArray[0];
    }

    public GameObject priorityTop(){
        GameObject[] filaArray = filaDePrioridade.ToArray();
        return filaArray[0];
    }


    public string[] criaReceita(){
        string[] receita = new string[3];

        for(int i = 0; i < 3; i++){
            int rand = Random.Range(0, 3);
            if(rand == 0){  
                receita[i] = "azul";
            }

            if(rand == 1){
                receita[i] = "vermelho";
            }

            if(rand == 2){
                receita[i] = "verde";
            }
        }

        return receita;
    }

    public void AdicionarCliente(GameObject novoCliente) {
        if (filaDeClientes.Count >= maxQueue){
            Destroy(novoCliente);
            return;
        }
        bool isPriority = false;



        novoCliente.GetComponent<Cliente>().receita = criaReceita();
        Debug.Log(novoCliente.GetComponent<Cliente>().receita[0] + " - " + novoCliente.GetComponent<Cliente>().receita[1] + " - " + novoCliente.GetComponent<Cliente>().receita[2]);
            
        Sprite randomSprite;

        int rand = Random.Range(0, 5);
        //Debug.Log(rand);
        if(rand == 0 && !clientePrioridadeEmFila){
            clientePrioridadeEmFila = true;
            isPriority = true;
            randomSprite = prioritySprite;
        } else {
            rand = Random.Range(0, 2);
            randomSprite = clientSprites[rand];
        }
        novoCliente.GetComponent<SpriteRenderer>().sprite = randomSprite;

        if(isPriority){
            filaDePrioridade.Enqueue(novoCliente);
        }else{
            novoCliente.transform.position = novoCliente.transform.position + new Vector3(1f, 0, 0);
            filaDeClientes.Enqueue(novoCliente);
        }

        if(isPriority){
            novoCliente.GetComponent<Cliente>().prioridade = true;
            novoCliente.GetComponent<Cliente>().alvoPosicao = balcaoPrioridade;
        }
        else if (filaDeClientes.Count == 1) {
            novoCliente.GetComponent<Cliente>().alvoPosicao = balcao;
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
            tempoParaSerAtendido = 65.0f;
            GameObject clienteRemovido = filaDeClientes.Dequeue();
            clienteRemovido.GetComponent<Cliente>().atendido = true;
            StartCoroutine(AnimacaoRemocaoCliente(clienteRemovido));
            if (filaDeClientes.Count > 0) AtualizarFila();
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


    public void resetBallons(GameObject cliente){

        cliente.GetComponent<Cliente>().removeAngryBalloon();
        cliente.GetComponent<Cliente>().removeAngryTimeBalloon();
        cliente.GetComponent<Cliente>().removeMediumBalloon();
        cliente.GetComponent<Cliente>().removeMoneyBalloon();
    }

    private IEnumerator AnimacaoRemocaoCliente(GameObject cliente) {
        resetBallons(cliente);


        if(!cliente.GetComponent<Cliente>().sucesso) {
            cliente.GetComponent<Cliente>().showAngryTimeBalloon();
        } else{
            cliente.GetComponent<Cliente>().showMoneyBalloon();
        }
        float duracaoAnimacaoX = 1.0f; // Duração da animação lateral 
        float duracaoAnimacaoY = 5.0f; // Duração da animação horizontal
        float distanciaMovimentoLateral = cliente.GetComponent<SpriteRenderer>().sprite == prioritySprite?-1:1; // Distância que o cliente se moverá lateralmente

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
        resetBallons(cliente);
        if(!cliente.GetComponent<Cliente>().sucesso){
            cliente.GetComponent<Cliente>().showAngryBalloon();
        } 


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
