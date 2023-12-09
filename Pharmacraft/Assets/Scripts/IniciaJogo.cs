using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciaJogo : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip ClickBotao;
    public void iniciaJogo() {
        Audio.clip = ClickBotao;
        Audio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
