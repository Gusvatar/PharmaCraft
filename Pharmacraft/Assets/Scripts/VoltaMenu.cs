using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltaMenu : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip ClickBotao;
    public void voltaMenu() {
        Audio.clip = ClickBotao;
        Audio.Play();
        SceneManager.LoadScene("Menu");
    }
}
