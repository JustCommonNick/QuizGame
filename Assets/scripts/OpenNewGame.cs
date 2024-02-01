using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenNewGame : MonoBehaviour
{
    public float _Inwoke;
    public void NewGame()
    {
        Invoke("InvokeNewGame", _Inwoke);
    }

    public void InvokeNewGame()
    {
        SceneManager.LoadScene("Game");
    }
}


