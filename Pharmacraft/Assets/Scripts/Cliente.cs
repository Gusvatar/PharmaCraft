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

    private GameObject angryBalloon;
    private GameObject angryTimeBalloon;
    private GameObject mediumBalloon;
    private GameObject moneyBalloon;


    private void Start()
    {
        angryBalloon = transform.GetChild(0).gameObject;
        angryTimeBalloon = transform.GetChild(1).gameObject;
        mediumBalloon = transform.GetChild(2).gameObject;
        moneyBalloon = transform.GetChild(3).gameObject;
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
