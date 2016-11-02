using UnityEngine;
using System.Collections;

public class ScriptEnd : ScriptGeneric
{
    public GameObject canvas;

    void Start()
    {
        Invoke("ShowCanvas", 3f);
    }

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }
}
