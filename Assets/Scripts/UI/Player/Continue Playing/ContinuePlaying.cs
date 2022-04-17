using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuePlaying : MonoBehaviour
{

    public void OnYes() => PlayerAutoSave.Instance.Continue();

    public void OnNo() => Application.Quit();
}
