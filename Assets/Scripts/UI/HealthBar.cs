using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _maxHealth;
    private float _minHealth = 0;
    private float _currentHealth;

    public Slider slider;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SetHealthBar(player.maxHealth);
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.currentHealth;
    }

    public void SetHealthBar(float value)
    {
        slider.minValue = this._minHealth;
        this._maxHealth = value;
        slider.maxValue = this._maxHealth;
    }

    public void SetHealth(float value)
    {
        this._currentHealth = value;
    }
}
