using UnityEngine;
using System.Collections;

public class ClassCore : ClassGeneric
{
    public int life;
    public GameObject bomb;
    public GameObject death;
    public float bombSpeed;
    public GameObject sparks;
    private Transform player;

    private GameObject gm;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        gm = GameObject.Find("GameManager");
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.name == "Bullet")
        {
            life -= c.GetComponent<ClassBullet>().dmg;
            BehaviourAnimation.Play(gameObject, "Hit");
            if (life <= 0)
            {
                Kill();
                death.Spawn(transform).transform.parent = null;
            }
        }

    }

    private void Kill()
    {
        GameObject obj;
        obj = sparks.Spawn(transform);
        obj.transform.parent = transform.parent;
        gameObject.SetActive(false);
        gm.GetComponent<ScriptGame>().DownCore();
        InvokeRepeating("Shoot",1f,2f);
    }

    private void Shoot()
    {
        GameObject obj;
        obj = bomb.Spawn(transform);
        obj.transform.parent = null;
        obj.GetComponent<Rigidbody2D>().velocity = bombSpeed * (player.position - obj.transform.position).normalized;
    }
}
