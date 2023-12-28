using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Sound : MonoBehaviour
{

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;
    // Start is called before the first frame update

    [SerializeField] private GameObject soundON;
    [SerializeField] private GameObject soundOFF;

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    public void GetLoad()
    {
        if (YandexGame.savesData.Sound)
        {  
            soundOFF.SetActive(true);
            soundON.SetActive(false);
            AudioListener.volume = 0;
            YandexGame.savesData.Sound = false;
        }
        else
        {
            soundOFF.SetActive(false);
            soundON.SetActive(true);
            AudioListener.volume = 1;
            YandexGame.savesData.Sound = true;
        }
    }
}
