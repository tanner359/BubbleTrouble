using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsSlider : MonoBehaviour
{
    public double floatSliderValue; 
    public int sliderValue; //int holding the specific number of the value 
    public TextMeshProUGUI sliderValueText; //text displaying volume Value; 
    public Slider settingsSlider;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
    //    sliderValueText = GameObject.Find("VolumeIndex").GetComponent<TextMeshProUGUI>();
    //}

    // Update is called once per frame
    void Update()
    {
        floatSliderValue = settingsSlider.value * 100.0;
        sliderValue = (int)floatSliderValue;
        sliderValueText.text = sliderValue.ToString(); 
    }
}
