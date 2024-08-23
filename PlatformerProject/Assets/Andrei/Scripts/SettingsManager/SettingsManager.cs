using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] GameObject qualityList;

    private void Start()
    {
        qualityList.SetActive(false);
    }

    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(0);
        Screen.SetResolution(480, 854, Screen.fullScreen);
    }

    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(2);
        Screen.SetResolution(720, 1280, Screen.fullScreen);
    }

    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1);
        Screen.SetResolution(1080, 1920, Screen.fullScreen);
    }
}
