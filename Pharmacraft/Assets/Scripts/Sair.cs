using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sair : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip ClickBotao;
    public void sairJogo() {
        Audio.clip = ClickBotao;
        Audio.Play();
        Application.Quit();
    }
}
