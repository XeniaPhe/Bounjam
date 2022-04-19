using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public static PlayerDeath Instance;

    public bool isFirst, isSecond, isCooldown;

    void Awake() => Instance = this;

    RaycastHit2D hit;
    void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, LayerMask.GetMask("Ground"));
        if (hit) isSecond = isFirst = false;
        else if (!hit && !isCooldown)
        {
            if ((isSecond = isFirst) == true) OnDeath();
            else isFirst = true;
            isCooldown = true;
            if(gameObject.activeSelf) StartCoroutine(EndCooldown());
        }
    }

    public IEnumerator EndCooldown()
    {
        yield return new WaitForSeconds(2f);
        isCooldown = false;
    }

    public void OnDeath() => PlayerStats.Instance.TakeDamage(PlayerStats.Instance.maxHealth);
}
