using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public float _Inwoke;
    public void Back()
    {
        Invoke("InwokeBack", _Inwoke);
    }

    public void InwokeBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}