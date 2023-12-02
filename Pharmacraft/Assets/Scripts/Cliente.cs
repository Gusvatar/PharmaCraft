using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    public GameObject alvoPosicao;
    public bool atendido = false;
    public float velocidade = 2.0f;
    public float distânciaParadaY = 1f;
    public bool prioridade = false;

    public GameObject balaoInsatisfacao;
    public GameObject balaoRaiva;

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

    public void showWarning(){
        balaoInsatisfacao.SetActive(true);
    }

    public void removeWarning(){
        balaoInsatisfacao.SetActive(false);
    }

    public void showDesapointment(){
        balaoRaiva.SetActive(true);
    }

    public void removeDesapointment(){
        balaoRaiva.SetActive(false);
    }
}
