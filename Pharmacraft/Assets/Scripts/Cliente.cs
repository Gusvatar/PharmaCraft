using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    public GameObject alvoPosicao;
    public bool atendido = false;
    public bool sucesso = false;
    public float velocidade = 2.0f;
    public float distânciaParadaY = 1f;
    public bool prioridade = false;

    public int[] receita = new int[3];



    private GameObject angryBalloon;
    private GameObject angryTimeBalloon;
    private GameObject mediumBalloon;
    private GameObject moneyBalloon;



    public Sprite[] recepySprites;
    private GameObject recepy1;
    private GameObject recepy2;
    private GameObject recepy3;
    public bool visibleRecepy = true;



    public int validRecepy(int[] remedio){
        int i = 0;

        for(int j=0;j<3;j++){
            if(remedio[j] == receita[j]) i++;
        }


        return i*10;
    }


    private void Start()
    {
        angryBalloon = transform.GetChild(0).gameObject;
        angryTimeBalloon = transform.GetChild(1).gameObject;
        mediumBalloon = transform.GetChild(2).gameObject;
        moneyBalloon = transform.GetChild(3).gameObject;

        recepy1 = transform.GetChild(4).gameObject;
        recepy2 = transform.GetChild(5).gameObject;
        recepy3 = transform.GetChild(6).gameObject;

        receita = criaReceita();
        loadRecepySprits();
    }

    public int[] criaReceita(){
        int[] receita = new int[3];

        for(int i = 0; i < 3; i++){
            int rand = Random.Range(0, 3);
            receita[i] = rand;
        }

        return receita;
    }


    public void showRecepy(){
        recepy1.SetActive(true);
        recepy2.SetActive(true);
        recepy3.SetActive(true);
    }

    public void closeRecepy(){
        recepy1.SetActive(false);
        recepy2.SetActive(false);
        recepy3.SetActive(false);
    }

    public void loadRecepySprits(){
        recepy1.GetComponent<SpriteRenderer>().sprite = recepySprites[receita[0]];
        recepy2.GetComponent<SpriteRenderer>().sprite = recepySprites[receita[1]];
        recepy3.GetComponent<SpriteRenderer>().sprite = recepySprites[receita[2]];
    }


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
            
            if(visibleRecepy && !atendido && (alvoPosicao.GetComponent<balcao>() || alvoPosicao.GetComponent<balcaoPreferencia>())) showRecepy();
            else closeRecepy();
            return;
        }else if (!atendido){
            velocidade = 2.0f;

        }

        direção.Normalize();

        // Move o cliente em direção à posição alvo
        transform.Translate(direção * velocidade * Time.deltaTime);
    }

    public void showAngryBalloon(){  angryBalloon.SetActive(true); }
    public void removeAngryBalloon(){ angryBalloon.SetActive(false); }

    public void showAngryTimeBalloon(){  angryTimeBalloon.SetActive(true); }
    public void removeAngryTimeBalloon(){ angryTimeBalloon.SetActive(false); }

    public void showMediumBalloon(){  mediumBalloon.SetActive(true); }
    public void removeMediumBalloon(){ mediumBalloon.SetActive(false); }

    public void showMoneyBalloon(){  moneyBalloon.SetActive(true); }
    public void removeMoneyBalloon(){ moneyBalloon.SetActive(false); }
}
