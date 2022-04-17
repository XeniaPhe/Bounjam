using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    bool isCooldown;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (isCooldown) return;
            AudioManager.Instance.PlayBattleMusic();
            isCooldown = true;
            StartCoroutine(EndCooldown());
        }
    }

    public IEnumerator EndCooldown()
    {
        yield return new WaitForSeconds(10f);
        isCooldown = false;
    }
}
