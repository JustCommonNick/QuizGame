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

    private void OnEnable() => YandexGame.GetDataEvent += GetLoadData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoadData;

    private void Awake()
    {
        GameLogic = FindObjectOfType<GameLogic>();
    }

    public void Start()
    {

        if (YandexGame.SDKEnabled == true)
        {
            GetLoadData();
        }
    }

    private void GetLoadData()
    {
        Themes = GameLogic.TakeData();
        if (levelid <= Themes.Length - 1)
        {
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Themes[levelid].ThemeName;
            if (YandexGame.savesData.Stars[levelid] > 0)
            {
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Stars/" + YandexGame.savesData.Stars[levelid] + "stars");
            }
            else
            {
                gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        GameLogic._levelid = levelid;
        mainPanel.SetActive(false);
        questionPanel.SetActive(true);
        GameLogic.StartQuestion();
        YandexGame.FullscreenShow();
    }

    public void Setlevelid(int _levelid) { levelid = _levelid; }
    public void SetmainPanel(GameObject _mainPanel) { mainPanel = _mainPanel; }
    public void SetquestionPanel(GameObject _questionPanel) { questionPanel = _questionPanel; }
}
