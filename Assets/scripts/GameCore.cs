using Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Core
{
    public class GameCore : MonoBehaviour
    {
        [Header("Questions")]
        [SerializeField] private QuestionsStruct[] questions;

        [Header("Save Config")]
        [SerializeField] private string savePath;
        [SerializeField] private string saveFileName = "data.json";

        public void SaveToFile()
        {
            GameCoreDataStruct gameCore = new GameCoreDataStruct
            {
                questions = this.questions
            };

            string json = JsonUtility.ToJson(gameCore, true);

            try
            {
                File.WriteAllText(savePath, json);
            }
            catch (Exception e)
            {
                Debug.Log("{GameLog} => [GameCore] - (<color=red>Error</color>) - SaveToFile -> " + e.Message);
            }
        }

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
                this.questions = gameCoreFromJson.questions;

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

        private void OnApplicationQuit()
        {
            SaveToFile();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                SaveToFile();
            }
        }
    }
}