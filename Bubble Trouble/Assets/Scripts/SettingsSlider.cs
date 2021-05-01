using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsSlider : MonoBehaviour
{
    public Slider volume_S, sensitivity_S;
    public TMPro.TMP_Text volumeNum, sensitivityNum;

    private void Awake()
    {
        volume_S.value = Settings.volume;
        sensitivity_S.value = Settings.sensitivity;

        UpdateVolume();
        UpdateSensitivity();
    }

    public void UpdateVolume()
    {
        Settings.ChangeVolume((int)volume_S.value);
        volume_S.value = Settings.volume;
        volumeNum.text = volume_S.value.ToString();
    }

    public void UpdateSensitivity()
    {
        Settings.ChangeSensitivity((int)sensitivity_S.value);
        sensitivity_S.value = Settings.sensitivity;
        sensitivityNum.text = sensitivity_S.value.ToString();
    }
}
