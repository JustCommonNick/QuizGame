using UnityEngine;
using Struct;
using Core;
using YG;

namespace CreateButtons
{
    public class CreateButtons : MonoBehaviour
    {
        private ThemeStruct[] Data;
        private GameLogic GameLogic;

        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject questionPanel;

        [SerializeField] private GameObject obj;
        private void Start()
        {
            GameLogic = FindObjectOfType<GameLogic>();
            Data = GameLogic.TakeData();
            Create();
            OpenVideoAd();
        }
        public void Create()
        {
            for (int i = 0; i < Data.Length; i++)
            {
                GameObject CreatedButton = Instantiate(obj) as GameObject;
                CreatedButton.transform.SetParent(this.gameObject.transform, false);
                CreatedButton.transform.GetComponent<LevelsManagement>().Setlevelid(i);
                CreatedButton.transform.GetComponent<LevelsManagement>().SetmainPanel(mainPanel);
                CreatedButton.transform.GetComponent<LevelsManagement>().SetquestionPanel(questionPanel);
            }
        }
        public void OpenVideoAd()
        {
            YandexGame.FullscreenShow();
        }
    }
}

