using UnityEngine;
using System.Collections;

public class ScriptSplash : ScriptGeneric
{
    private GameObject load;
    public GameObject canvas;
    public AudioSource up;

    void Start()
    {
        load = GameObject.Find("Load");
        Invoke("ShowCanvas", 3f);
    }

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }

    public void StartGame()
    {
        up.Play();
        canvas.SetActive(false);
        load.GetComponent<ScriptLoad>().FadeOut("Menu");
    }
}
