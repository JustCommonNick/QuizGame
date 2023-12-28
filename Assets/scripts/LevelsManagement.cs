using UnityEngine;
using Core;
using Struct;
using TMPro;
using UnityEngine.UI;
using YG;

public class LevelsManagement : MonoBehaviour
{
    [SerializeField] private int levelid;
    private ThemeStruct[] Themes;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject questionPanel;
    private GameLogic GameLogic;

    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        GameLogic = FindObjectOfType<GameLogic>();
    }

    public void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    private void GetLoad()
    {
        Themes = GameLogic.TakeData();
        if (levelid <= Themes.Length - 1)
        {
            this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Themes[levelid].ThemeName;

            if (YandexGame.savesData.Stars[levelid] > 0)
            {
                this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Stars/" + YandexGame.savesData.Stars[levelid] + "stars");
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
