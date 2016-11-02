using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManagerGeneric
{
    private static GameManagerGeneric  gameManagerGeneric;

    private Dictionary<string, object> info = new Dictionary<string, object>();

    private GameManagerGeneric(){}

    public static GameManagerGeneric Instance
    {
        get
        {
            if (gameManagerGeneric == null)
            {
                gameManagerGeneric = new GameManagerGeneric();
            }
            return gameManagerGeneric;
        }
    }

    public void SaveInfo(Dictionary<string, object> info)
    {
        foreach(string key in info.Keys)
        {
            //Case already contain key, replace value
            if(this.info.ContainsKey(key))
            {
                this.info[key] = info[key];
            }
            //Case do not conatin key yet, add value
            else
            {
                this.info.Add(key, info[key]);
            }
        }
    }

    public Dictionary<string, object> GetInfo(List<string> keys)
    {
        Dictionary<string, object> info = new Dictionary<string,object>();
        
        foreach(string key in keys)
        {
            if(this.info.ContainsKey(key))
            {
                info.Add(key, this.info[key]);
            }
            else
            {
                info.Add(key, null);
            }
        }

        return info;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
