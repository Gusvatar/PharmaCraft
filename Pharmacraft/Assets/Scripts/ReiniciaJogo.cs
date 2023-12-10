using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciaJogo : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip ClickBotao;
   public void reiniciaJogo() {
        Audio.clip = ClickBotao;
        Audio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
