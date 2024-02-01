using Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class Sound : MonoBehaviour
{

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;
    // Start is called before the first frame update

    public AudioMixerGroup Mixer;

    public Toggle toggle;

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    public void GetLoad()
    {
        if (!YandexGame.savesData.Sound)
        {
            toggle.isOn = YandexGame.savesData.Sound == true;
        }
    }

    public void SetToggleSound(bool enable)
    {
        enable = toggle.isOn;
        if (enable)
        {
            Mixer.audioMixer.SetFloat("MasterVolume", 0);
            YandexGame.savesData.Sound = true;
        }
        else
        {
            Mixer.audioMixer.SetFloat("MasterVolume", -80);
            YandexGame.savesData.Sound = false;
        }
    }
}
