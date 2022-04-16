using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AIPath aiPath;

    private void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else if(aiPath.desiredVelocity.x <= -0.01f) transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
