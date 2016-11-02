using UnityEngine;
using System.Collections;

public class ClassBullet : ClassGeneric
{
    public GameObject hit;
    public int dmg;

    void Awake()
    {
        name = "Bullet";
        dmg = 1 + (1 * PlayerPrefs.GetInt("AT", 0));
        //lv.0 1
        //lv.1 1
        //lv.2 2
        //lv.3 4
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Player") && !c.CompareTag("Bullet") && !c.CompareTag("Wave"))
        {
            gameObject.Recycle();
            hit.Spawn(transform.position);
        }
    }

    void OnEnable()
    {
        BehaviourAnimation.Play(gameObject, "Flash");
    }
}
