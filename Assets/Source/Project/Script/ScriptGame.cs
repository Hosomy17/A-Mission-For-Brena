using UnityEngine;
using System.Collections;

public class ScriptGame : ScriptGeneric
{
    public GameObject player;
    public GameObject boss;
    private GameObject cre0;
    private GameObject cre00;
    private GameObject cre000;

    private GameObject dis0;
    private GameObject dis00;
    private GameObject dis000;

    public GameObject[] waves;
    public Transform spawnPoint;
    private float timeSpawn;
    private float speedSpawn;

    private int creCache;
    private int totalCre;
    private int distance;

    private GameObject lvHP;
    private GameObject lvSP;
    private GameObject lvAT;

    private int cores;

    private GameObject load;
    private GameObject[] alerts;

    public AudioSource levelMusic;
    public AudioSource bossMusic;

    public GameObject deathBoss;

    private bool downVolume;
	
    void Start ()
    {
        cores = 6;
        alerts = GameObject.FindGameObjectsWithTag("Alert");
        load = GameObject.Find("Load");
        ControllerGeneric cp = new ControllerPlayer();
        cp.TrackObject(player);
        GetComponent<Gamepad>().controller = cp;

        totalCre = 0;

        distance = 1000;
        int booster = PlayerPrefs.GetInt("DIS", 0);
        switch(booster)
        {
            case 0:
                distance = 1000;
                timeSpawn = 17f;
                speedSpawn = 25f;
                break;
            case 1:
                distance = 500;
                timeSpawn = 12f;
                speedSpawn = 25f;
                break;
            case 2:
                distance = 110;
                timeSpawn = 10f;
                speedSpawn = 30f;
                break;
        }

        cre0 = GameObject.Find("Cre/0");
        cre00 = GameObject.Find("Cre/00");
        cre000 = GameObject.Find("Cre/000");

        dis0 = GameObject.Find("Dis/0");
        dis00 = GameObject.Find("Dis/00");
        dis000 = GameObject.Find("Dis/000");

        lvHP = GameObject.Find("Lv./HP");
        lvSP = GameObject.Find("Lv./SP");
        lvAT = GameObject.Find("Lv./AT");

        int sp = PlayerPrefs.GetInt("SP",0);
        int at = PlayerPrefs.GetInt("AT", 0);
        int hp = PlayerPrefs.GetInt("HP", 0);

        timeSpawn -= sp + at;

        float aux = 1;
        switch(sp)
        {
            case 0:
                aux = 1;
                break;
            case 1:
                aux = 2.083f;
                break;
            case 2:
                aux = 3.33f;
                break;
            case 3:
                aux = 5.55f;
                break;
        }
        //lv.0 1
        //lv.1 0.48
        //lv.2 0.3
        //lv.3 0.18
        BehaviourAnimation.Play(lvSP, sp.ToString());
        BehaviourAnimation.Play(lvHP, hp.ToString());
        BehaviourAnimation.Play(lvAT, at.ToString());
        InvokeRepeating("UpDistance", 1f, 1/aux);

        InvokeRepeating("SpawnWave", 0, timeSpawn);

        downVolume = false;
        float timeMusic = 330 - distance * (1 / aux);
        if (timeMusic > 0)
            levelMusic.time = timeMusic;
        levelMusic.Play();
	}

    public void DownCore()
    {
        cores--;
        GiveCre(150);
        if(cores <= 0)
        {
            GameObject obj;
            obj = deathBoss.Spawn(transform);
            obj.transform.localScale = Vector3.one * 3;
            obj.transform.parent = null;

            Destroy(boss);
            CancelInvoke("SpawnWave");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject it in enemies)
            {
                it.GetComponent<ClassEnemy>().Kill();
            }
            Invoke("Ending", 3f);
        }
    }

    private void Ending()
    {
        PlayerPrefs.DeleteAll();
        downVolume = true;
        if (Input.GetKey(KeyCode.H))
            load.GetComponent<ScriptLoad>().FadeOut("End0");
        else
            load.GetComponent<ScriptLoad>().FadeOut("End");
    }

	void Update ()
    {
        if(creCache > 0)
        {
            totalCre++;
            creCache--;
            int aux = totalCre;
            BehaviourAnimation.Play(cre0, (aux % 10).ToString());
            aux /= 10;
            BehaviourAnimation.Play(cre00, (aux % 10).ToString());
            aux /= 10;
            BehaviourAnimation.Play(cre000, (aux % 10).ToString());
        }

        if(downVolume)
        {
            bossMusic.volume -= 0.01f;
            levelMusic.volume -= 0.01f;
        }
        else
        {
            levelMusic.volume += 0.01f;
        }
	}

    public void GiveCre(int give)
    {
        creCache += give;
    }

    private void UpDistance()
    {
        distance--;
        int aux = distance;
        BehaviourAnimation.Play(dis0,(aux % 10).ToString());
        aux /= 10;
        BehaviourAnimation.Play(dis00,(aux % 10).ToString());
        aux /= 10;
        BehaviourAnimation.Play(dis000,(aux % 10).ToString());

        if (distance % 50 == 0)
        {
            timeSpawn -= 0.5f;
            speedSpawn++;
            CancelInvoke("SpawnWave");
            InvokeRepeating("SpawnWave", 0, timeSpawn);
        }
        if(distance == 0)
        {
            CancelInvoke();
            SpawnBoss();
            InvokeRepeating("SpawnWave", 5, 10);
            speedSpawn = 40f;
        }

    }

    private void SpawnBoss()
    {
        levelMusic.Pause();
        bossMusic.Play();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject it in enemies)
        {
            it.GetComponent<ClassEnemy>().Kill();
        }

        foreach(GameObject it in alerts)
        {
            BehaviourAnimation.Play(it, "alert");
        }

        BehaviourAnimation.Play(boss, "Drop");
    }

    private void SpawnWave()
    {
        int id = Random.Range(0,waves.Length);
        GameObject obj = waves[id].Spawn(spawnPoint);
        BehaviourPhysics.Move(obj, Vector2.down, speedSpawn);
    }

    public void GameOver()
    {
        downVolume = true;
        int cre = totalCre + creCache;
        int total = PlayerPrefs.GetInt("CRE", 0);
        total += cre;
        PlayerPrefs.SetInt("CRE", total);
        load.GetComponent<ScriptLoad>().FadeOut("Menu");
    }
}