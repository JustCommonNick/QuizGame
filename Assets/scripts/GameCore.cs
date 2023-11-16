using Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

namespace Core
{
    public class GameCore : MonoBehaviour
    {
        [Header("Questions")]
        [SerializeField] private QuestionsStruct[] questions;

        [Header("Save Config")]
        [SerializeField] private string savePath;
        [SerializeField] private string saveFileName = "data.json";

        [SerializeField] private int count_questions;
        [SerializeField] private int current_question;

        [SerializeField] private string question;
        [SerializeField] private Answers[] answer;
        [SerializeField] private string background;

        [SerializeField] private int count_correct_answers;

        public GameObject results;
        public GameObject count_correct_answers_label;
        public GameObject questioncount_label;
        public GameObject question_label;

        public GameObject answer_label1;
        public GameObject answer_label2;
        public GameObject answer_label3;
        public GameObject answer_label4;

        public GameObject result_answer;
        public GameObject buttons_group;
        public GameObject result_answer_label;

        public Image backgroundImage;


        //public void SaveToFile()
        //{
        //    GameCoreDataStruct gameCore = new GameCoreDataStruct
        //    {
        //        questions = this.questions
        //    };

        //    string json = JsonUtility.ToJson(gameCore, true);

        //    try
        //    {
        //        File.WriteAllText(savePath, json);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Log("{GameLog} => [GameCore] - (<color=red>Error</color>) - SaveToFile -> " + e.Message);
        //    }
        //}

        public void LoadFromFile()
        {
            if (!File.Exists(savePath))
            {
                Debug.Log("{GameLog} => [GameCore] - LoadFromFile -> File Not Found!");
                return;
            }

            try
            {
                string json = File.ReadAllText(savePath);

                GameCoreDataStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreDataStruct>(json);
                questions = gameCoreFromJson.questions;

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
            savePath = Path.Combine(Application.dataPath, saveFileName);
#endif
            LoadFromFile();
        }

        //private void OnApplicationQuit()
        //{
        //    SaveToFile();
        //}

        //private void OnApplicationPause(bool pauseStatus)
        //{
        //    if (Application.platform == RuntimePlatform.Android)
        //    {
        //        SaveToFile();
        //    }
        //}

        private void Start()
        {
            count_questions = questions.Length;
            current_question = 0;
            Questions();

        }

        public void Questions()
        {
            if (current_question + 1 <= count_questions)
            {
                buttons_group.SetActive(true);
                result_answer.SetActive(false);
                question = questions[current_question].queston;
                question_label.GetComponent<TextMeshProUGUI>().text = question;
                answer = questions[current_question].answers;
                answer_label1.GetComponent<TextMeshProUGUI>().text = answer[0].text;
                answer_label2.GetComponent<TextMeshProUGUI>().text = answer[1].text;
                answer_label3.GetComponent<TextMeshProUGUI>().text = answer[2].text;
                answer_label4.GetComponent<TextMeshProUGUI>().text = answer[3].text;
                background = questions[current_question].background;
                Gamebackground();
                questioncount_label.GetComponent<TextMeshProUGUI>().text = $"Вопрос: {current_question + 1}/{count_questions}";
                current_question += 1;


            }
            else
            {
                Debug.Log("Вопросы закончились");
                results.SetActive(true);
                count_correct_answers_label.GetComponent<TextMeshProUGUI>().text = $"{count_correct_answers}/{count_questions}";
            }

        }

        public void Answer1()
        {
            if (answer[0].correct == true)
            {
                count_correct_answers += 1;
                CorretResult();
            }
            else
            {
                NotCorretResult();
            }
            buttons_group.SetActive(false);
            result_answer.SetActive(true);
        }

        public void Answer2()
        {
            if (answer[1].correct == true)
            {
                count_correct_answers += 1;
                CorretResult();
            }
            else
            {
                NotCorretResult();
            }
            buttons_group.SetActive(false);
            result_answer.SetActive(true);
        }

        public void Answer3()
        {
            if (answer[2].correct == true)
            {
                count_correct_answers += 1;
                CorretResult();
            }
            else
            {
                NotCorretResult();
            }
            buttons_group.SetActive(false);
            result_answer.SetActive(true);
        }
        public void Answer4()
        {
            if (answer[3].correct == true)
            {
                count_correct_answers += 1;
                CorretResult();
            }
            else
            {
                NotCorretResult();
            }
            buttons_group.SetActive(false);
            result_answer.SetActive(true);
        }

        public void Gamebackground()
        {
            {
                background = background.Substring(0, background.Length - 4);

                Sprite newImage = Resources.Load<Sprite>(background);

                if (newImage != null)
                {
                    backgroundImage.sprite = newImage;
                }
                else
                {
                    Debug.LogError($"Ошибка загрузки фона: {background}");
                }
            }
        }

        public void CorretResult()
        {
            result_answer_label.GetComponent<TextMeshProUGUI>().text = "Верно";
            result_answer_label.GetComponent<TextMeshProUGUI>().color = new Color(0.75f, 1f, 0.75f);
        }

        public void NotCorretResult()
        {
            result_answer_label.GetComponent<TextMeshProUGUI>().text = "Не верно";
            result_answer_label.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0.75f, 0.75f);
        }
    }
}