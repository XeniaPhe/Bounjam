using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public Player player;
    public UnityEngine.UI.Slider slider;
    public GameObject sliderComponents;

    public void OnHealthBarChange() => sliderComponents.SetActive((slider.value = PlayerStats.Instance.health / PlayerStats.Instance.maxHealth) < 1 ? true : false);

    private void Update() => transform.parent.parent.position = new Vector2(player.transform.position.x - 1.3f, player.transform.position.y);
}
