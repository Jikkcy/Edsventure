using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    // Start is called before the first frame update
    public Image appleSkill;
    public PlayerController player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        appleSkill.fillAmount = (player.coolDown + 10) / 10;
    }
}
