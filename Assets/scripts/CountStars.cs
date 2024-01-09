using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YG;

namespace Calc
{
    public class CountStars : MonoBehaviour
    {
        public int _CountStars;
        private void OnEnable() => YandexGame.GetDataEvent += CalculateStars;

        private void OnDisable() => YandexGame.GetDataEvent -= CalculateStars;

        void Start()
        {
            if (YandexGame.SDKEnabled == true)
            {
                CalculateStars();
            }
        }

        public void CalculateStars()
        {
            GetCountStars();
            this.gameObject.GetComponent<TextMeshProUGUI>().text = _CountStars.ToString();
        }

        public void GetCountStars()
        {
            for (int i = 0; i < YandexGame.savesData.Stars.Count(); i++)
            {
                _CountStars += YandexGame.savesData.Stars[i];
            }
        }
    }
}

