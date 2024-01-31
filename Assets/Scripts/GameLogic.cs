using Struct;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using System.Collections.Generic;
using YG;
using System.Linq;
using Calc;

namespace Core
{
    enum ResultType
    {
        Correct,
        Uncorrect
    }

	public class GameLogic : MonoBehaviour
    {
        [Header("Themes")]
        [SerializeField] private ThemeStruct[] Themes;
        [Header("Questions")]
        private QuestionsStruct[] questions;

        [Header("Save Config")]
        [SerializeField] private string _savePath;
        private string _saveFileName = "data.json";
        private string _backgroundPath = "backgrounds/";

        private int _countQuestions;
        private int _currentQuestion;

        private string _question;
        private Answers[] _answer;
        private string _background;

        private int _countCorrectAnswers;

        public int _levelid;

        [Header("Панели для отбражения")]
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private GameObject resultPanelBetweenQuestions;
        [SerializeField] private GameObject questionsAndAnswersPanel;

        [Header("Текстовые поля")]
        [SerializeField] private TextMeshProUGUI CountCorrectAnswersText;
        [SerializeField] private TextMeshProUGUI correctQuestionsText;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private TextMeshProUGUI resultTextBetweenQuestions;

        [Header("Текстовые поля для ответов")]
        [SerializeField] private List<TextMeshProUGUI> answersText;

        [Header("Изображения")]
        [SerializeField] private Image backgroundImage;

        [Header("Настройки прцентности")]
        [SerializeField] private float OneStars;
        [SerializeField] private float TwoStars;
        [SerializeField] private float ThreeStars;

        private int _CountStars;

        private  CountStars CountStars;

        public ThemeStruct[] TakeData() 
        { 
           return Themes; 
        }


        //Для сохранения данных в Json
        /*        public void SaveToFile()
                {
                    GameCoreDataStruct gameCore = new GameCoreDataStruct
                    {
                        Themes = this.Themes
                    };

                    string json = JsonUtility.ToJson(gameCore, true);

                    try
                    {
                        File.WriteAllText(_savePath, json);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("{GameLog} => [GameCore] - (<color=red>Error</color>) - SaveToFile -> " + e.Message);
                    }
                }*/

        //Загрузка данных напрямую из файла json
        /*private void LoadFromFile()
        {
            if (!File.Exists(_savePath))
            {
                Debug.Log("{GameLog} => [GameCore] - LoadFromFile -> File Not Found!");
                return;
            }

            try
            {
                string json = File.ReadAllText(_savePath);

                GameCoreDataStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreDataStruct>(json);
                Themes = gameCoreFromJson.Themes;

            }
            catch (Exception e)
            {
                Debug.Log("{GameLog} - [GameCore] - (<color=red>Error</color>) - LoadFromFile -> " + e.Message);
            }
        }*/

        //При выходе из приложения сохраняются данные
        /*        private void OnApplicationQuit()
                {
                    SaveToFile();
                }

                private void OnApplicationPause(bool pauseStatus)
                {
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        SaveToFile();
                    }
                }*/
        private void LoadFromFile()
        {
            string jsonData = Resources.Load<TextAsset>("Data/data").ToString();

                if (jsonData == null)
            {
                Debug.Log("{GameLog} => [GameCore] - LoadFromFile -> File Not Found!");
                return;
            }

            try
            {
                GameCoreDataStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreDataStruct>(jsonData);
                Themes = gameCoreFromJson.Themes;

            }
            catch (Exception e)
            {
                Debug.Log("{GameLog} - [GameCore] - (<color=red>Error</color>) - LoadFromFile -> " + e.Message);
            }
        }

        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
            _savePath = Path.Combine(Application.dataPath, _saveFileName);
#endif
            LoadFromFile();
        }

        void Start()
        {
            _currentQuestion = 0;

            // Добавляем значения 0 равное количеству тем, для того, чтобы избежать ошибок.
            if (YandexGame.savesData.Stars == null)
            {
                YandexGame.savesData.Stars = new int[20];
            }
            CountStars = FindObjectOfType<CountStars>();
        }

        public void StartQuestion()
        {
            questions = Themes[_levelid].questions;
            _countQuestions = questions.Length;
            LoadQuestion();
            //LoadBackground();
        }

        public void LoadQuestion()
        {
            if (_currentQuestion + 1 <= _countQuestions)
            {
                ShowQuestionPanels();

                LoadQuestionData();

                _currentQuestion += 1;

                UpdateTextFields();
            }
            else
            {
                FinishTheQuiz();
            }
        }

        public void CheckAnswer(int answerNumber)
        {
			if (_answer[answerNumber].correct)
			{
				_countCorrectAnswers += 1;
				CheckResult(ResultType.Correct);
			}
			else
			{
				CheckResult(ResultType.Uncorrect);
			}
			questionsAndAnswersPanel.SetActive(false);
            resultPanelBetweenQuestions.SetActive(true);
		}

        private void ShowQuestionPanels()
        {
			questionsAndAnswersPanel.SetActive(true);
			resultPanelBetweenQuestions.SetActive(false);
		}

        private void LoadQuestionData()
        {
			_question = questions[_currentQuestion].queston;
			_answer = questions[_currentQuestion].answers;
			_background = Path.GetFileNameWithoutExtension(Themes[_levelid].background);
		}

/*        private void LoadBackground()
        {
            {
                _background = _backgroundPath + _background;
                Sprite newImage = Resources.Load<Sprite>(_background);

                if (newImage != null)
                {
                    backgroundImage.sprite = newImage;
                }
                else
                {
                    Debug.LogError($"Ошибка загрузки фона: {_background}");
                }
            }
        }*/

        private void UpdateTextFields()
        {
			for (int i = 0; i < answersText.Count; i++)
			{
				answersText[i].text = _answer[i].text;
			}
			questionText.text = _question;
			correctQuestionsText.text = String.Format("Вопрос: {0}/{1}", _currentQuestion, _countQuestions);
		}

        private void FinishTheQuiz()
        {
            float _countCorrectAnswersInPercents;
            _countCorrectAnswersInPercents = _countCorrectAnswers / _countQuestions * 100;
            Debug.Log("Вопросы закончились");
			resultsPanel.SetActive(true);
			CountCorrectAnswersText.text = String.Format("{0}/{1}", _countCorrectAnswers, _countQuestions);

            //сохранение звезд в data.json
/*           if ((_countCorrectAnswersInPercents >= ThreeStars) && (Themes[_levelid].stars < 3))
            {
                Themes[_levelid].stars = 3;
            }
            else if ((_countCorrectAnswersInPercents <= ThreeStars) && (TwoStars <= _countCorrectAnswersInPercents) && (Themes[_levelid].stars < 2))
            {
                Themes[_levelid].stars = 2;
            }
            else if ((_countCorrectAnswersInPercents <= TwoStars) && (OneStars <= _countCorrectAnswersInPercents) && (Themes[_levelid].stars < 1))
            {
                Themes[_levelid].stars = 1;
            }*/

            //сохранение на облако ЯндексИгры
            if ((_countCorrectAnswersInPercents >= ThreeStars) && (YandexGame.savesData.Stars[_levelid] < 3))
            {
                YandexGame.savesData.Stars[_levelid] = 3;
            }
            else if ((_countCorrectAnswersInPercents <= ThreeStars) && (TwoStars <= _countCorrectAnswersInPercents) && (YandexGame.savesData.Stars[_levelid] < 2))
            {
                YandexGame.savesData.Stars[_levelid] = 2;
            }
            else if ((_countCorrectAnswersInPercents <= TwoStars) && (OneStars <= _countCorrectAnswersInPercents) && (YandexGame.savesData.Stars[_levelid] < 1))
            {
                YandexGame.savesData.Stars[_levelid] = 1;
            }
            
            CountStars.CalculateStars();
            

            YandexGame.SaveProgress();

            for (int i = 0; i < YandexGame.savesData.Stars.Count(); i++)
            {
                _CountStars += YandexGame.savesData.Stars[i];
            }

            YandexGame.NewLeaderboardScores("BestMaxStars", _CountStars);

            YandexGame.FullscreenShow();
        }

        private void CheckResult(ResultType resultType)
        {
            switch (resultType)
            {
                case ResultType.Correct:
					resultTextBetweenQuestions.text = "Верно";
					resultTextBetweenQuestions.color = new Color(0.75f, 1f, 0.75f);
					break;
                case ResultType.Uncorrect:
					resultTextBetweenQuestions.text = "Неверно";
					resultTextBetweenQuestions.color = new Color(1f, 0.75f, 0.75f);
					break;
            }
        }
    }
}