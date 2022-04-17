using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyAI enemyAI;
    public UnityEngine.UI.Slider slider;
    public GameObject sliderComponents;

    public void OnHealthBarChange() => sliderComponents.SetActive((slider.value = enemyAI.health / enemyAI.maxHealth) < 1 ? true : false);

    private void Start() => sliderComponents.SetActive((slider.value = enemyAI.health / enemyAI.maxHealth) < 1 ? true : false);

    private void Update() => transform.parent.parent.position = enemyAI.transform.position;
}
