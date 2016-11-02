using UnityEngine;
using System.Collections;

public class ClassEnemy : ClassGeneric
{
    private float life;
    public GameObject death;
    public GameObject bullet;
    private Transform player;
    public float bulletSpeed;
    private ScriptGame sg;
    private float fireRate;
    private float maxLife;
    private float timeShoot;
    
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        sg = GameObject.Find("GameManager").GetComponent<ScriptGame>();
        timeShoot = Random.Range(1f, 10f);
        int aux = PlayerPrefs.GetInt("AT", 0);
        fireRate = 4.5f - aux;
        maxLife = 30 + aux * 40;
        life = maxLife;
    }

    void OnEnable()
    {
        BehaviourAnimation.Play(gameObject, "Hit");
        InvokeRepeating("Shoot", timeShoot, fireRate);
        life = maxLife;
    }

    void OnDisable()
    {
        CancelInvoke("Shoot");
    }

    void OnTriggerEnter2D (Collider2D c)
    {
        if (c.name == "Bullet")
        {
            life -= c.GetComponent<ClassBullet>().dmg;
            BehaviourAnimation.Play(gameObject, "Hit");
            if (life <= 0)
            {
                sg.GiveCre(10);
                Kill();
            }
        }
        else if(c.name == "Player")
        {
            sg.GiveCre(5);
            Kill();
        }
    }

    public void Kill()
    {
        GameObject obj;
        obj = death.Spawn(transform);
        obj.transform.parent = null;
        gameObject.Recycle();
    }

    public void Shoot()
    {
        GameObject obj;
        obj = bullet.Spawn(transform);
        obj.transform.parent = null;
        obj.GetComponent<Rigidbody2D>().velocity = bulletSpeed * (player.position - obj.transform.position).normalized;
    }
}
