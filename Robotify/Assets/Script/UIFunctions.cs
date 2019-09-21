using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    public void QuitButton()
    {
        Application.Quit();
    }
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}