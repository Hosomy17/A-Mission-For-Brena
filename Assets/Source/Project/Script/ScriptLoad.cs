using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScriptLoad : ScriptGeneric {

    private AsyncOperation asyncOpe;
    private int scale;

	void Awake()
    {
        scale = 3;
        Screen.SetResolution(320, 288, false);
        DontDestroyOnLoad(gameObject);
        FadeOut("Splash");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(scale)
            {
                case 1:
                    Screen.SetResolution(160, 144, false);
                    scale = 2;
                    break;
                case 2:
                    Screen.SetResolution(320, 288, false);
                    scale = 3;
                    break;
                case 3:
                    Screen.SetResolution(480, 432, false);
                    scale = 1;
                    break;
            }
        }
    }

    public void FadeOut(string scene)
    {
        BehaviourAnimation.Play(gameObject,"FadeOut");
        asyncOpe = SceneManager.LoadSceneAsync(scene);
        asyncOpe.allowSceneActivation = false;
        Invoke("LoadScene", 2f);
        Invoke("FadeIn", 2f);
    }

    private void LoadScene()
    {
        asyncOpe.allowSceneActivation = true;
    }

    private void FadeIn()
    {
        if (asyncOpe.isDone)
        {
            BehaviourAnimation.Play(gameObject, "FadeIn");
        }
        else
        {
            Invoke("FadeIn", 1f);
        }
    }
}
