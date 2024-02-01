using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMainMenu : MonoBehaviour
{
    public float _Inwoke;
    public void MainMenu()
    {
        Invoke("InvokeMainMenu", _Inwoke);
    }

    public void InvokeMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
