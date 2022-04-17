using System.Collections;
using UnityEngine;

public class FancyItems : MonoBehaviour
{
    [SerializeField] int frameCount;
    [SerializeField] int desiredFps;
    [SerializeField] float lowDistance;
    [SerializeField] float highDistance;

    WaitForSeconds oneOverFps;
    float delta;
    int highFrames;
    int lowFrames;
    bool turn=false;
    bool oldTurn=true;

    public void Start()
    {
        float upper = transform.position.y + highDistance;
        float lower = transform.position.y - lowDistance;
        float range = lowDistance + highDistance;
        lowFrames = (int)((lowDistance / range) * frameCount);
        highFrames = (int)((highDistance / range) * frameCount);
        delta = range / frameCount;
        oneOverFps = new WaitForSeconds(1f / desiredFps);
    }

    public void Update()
    {
        if(turn!=oldTurn)
        {
            oldTurn = turn;
            if (turn)
                StartCoroutine(AnimateDownwards());
            else
                StartCoroutine(AnimateUpwards());
        }
    }

    IEnumerator AnimateDownwards()
    {
        for (int i = 0; i < lowFrames; i++)
        {
            transform.Translate(Vector3.down * delta);
            yield return oneOverFps;
        }

        yield return oneOverFps;
        turn = !turn;
    }

    IEnumerator AnimateUpwards()
    {
        for (int i = 0; i < highFrames; i++)
        {
            transform.Translate(Vector3.up * delta);
            yield return oneOverFps;
        }

        yield return oneOverFps;
        turn = !turn;
    }
}