using UnityEngine;
using UnityEngine.SceneManagement;
using Core;

public class RestartGame : MonoBehaviour
{

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject questionPanel;
    private GameLogic GameLogic;

    public void Start()
    {
        GameLogic = FindObjectOfType<GameLogic>();
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame(int levelid)
    {
        GameLogic._levelid = levelid;
        mainPanel.SetActive(false);
        questionPanel.SetActive(true);
        GameLogic.StartQuestion();
    }
}
