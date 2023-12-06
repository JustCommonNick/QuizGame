using UnityEngine;
using Core;
using Struct;
using TMPro;
using UnityEngine.UI;

public class LevelsManagement : MonoBehaviour
{
    [SerializeField] private int levelid;
    private ThemeStruct[] Themes;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject questionPanel;
    private GameLogic GameLogic;

    private void Awake()
    {
        GameLogic = FindObjectOfType<GameLogic>();
    }

    public void Start()
    {
        Themes = GameLogic.TakeData();
        if (levelid <= Themes.Length - 1)
        {
            this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Themes[levelid].ThemeName;

            if (Themes[levelid].stars > 0)
            {
                this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Stars/" + Themes[levelid].stars + "stars");
            }
            else
            {
                this.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    public void StartGame()
    {
        GameLogic._levelid = levelid;
        mainPanel.SetActive(false);
        questionPanel.SetActive(true);
        GameLogic.StartQuestion();
    }

    public void Setlevelid(int _levelid) { levelid = _levelid; }
    public void SetmainPanel(GameObject _mainPanel) { mainPanel = _mainPanel; }
    public void SetquestionPanel(GameObject _questionPanel) { questionPanel = _questionPanel; }
}
