using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class slog : MonoBehaviour
{
	public static slog inst;
    public TMP_Text uiLog;

    void Awake()
    {
        if(inst != null && inst != this) Destroy(this);
        inst = this;
        StartCoroutine(DebugDelay());
    }

    public void Print(string _s)
    {
        uiLog.text += $"\n{_s}";
    }

    private IEnumerator DebugDelay()
    {
        yield return new WaitForSeconds(1f);
    }
}
