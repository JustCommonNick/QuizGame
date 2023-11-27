using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using Struct;

public class LevelsManagement : MonoBehaviour
{
    private ThemeStruct[] Themes;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject questionPanel;
    private GameLogic GameLogic;

    public void Start()
    {
        //GameLogic.TakeData(Themes);
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
