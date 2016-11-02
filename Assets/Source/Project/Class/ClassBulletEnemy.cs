using UnityEngine;
using System.Collections;

public class ClassBulletEnemy : ClassGeneric
{


    void Awake()
    {
        name = "Bullet Enemy";
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player") || c.name == "Boulders")
        {

            gameObject.Recycle();
        }
    }

}
