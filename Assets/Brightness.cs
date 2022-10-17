using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    public Slider Light_mode;
    public float sliderValue;
    public Image Panel_Light;

    // Start is called before the first frame update
    void Start()
    {
        Light_mode.value = PlayerPrefs.GetFloat("brillo", 0.5f);

        Panel_Light.color = new Color(Panel_Light.color.r, Panel_Light.color.g, Panel_Light.color.b, Light_mode.value);
    }

    //Update is called once per frame
    void Update()
    {

    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        Panel_Light.color = new Color(Panel_Light.color.r, Panel_Light.color.g, Panel_Light.color.b, Light_mode.value);
    }
}
