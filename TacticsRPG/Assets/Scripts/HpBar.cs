using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public BattleUnit battleUnit;
    public Slider hpSlider;
    public Slider apSlider;

    public void Awake()
    {
        hpSlider.maxValue = battleUnit.unitStats.maxHP;
        hpSlider.value = battleUnit.unitStats.currentHP;

        apSlider.maxValue = battleUnit.unitStats.maxAP;
        apSlider.value = battleUnit.unitStats.currentAP;
    }

    public void Update()
    {
        hpSlider.value = battleUnit.unitStats.currentHP;
        apSlider.value = battleUnit.unitStats.currentAP;
    }
}
