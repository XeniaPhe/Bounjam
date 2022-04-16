using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public Player player;
    public UnityEngine.UI.Slider slider;
    public GameObject sliderComponents;

    public void OnHealthBarChange(int value, int maxValue) => sliderComponents.SetActive((slider.value = value / (float)maxValue) < 1 ? true : false);

    private void Update() => transform.parent.parent.position = player.transform.position;
}
