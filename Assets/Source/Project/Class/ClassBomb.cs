using UnityEngine;
using System.Collections;

public class ClassBomb : ClassGeneric
{

    void Awake()
    {
        name = "Bomb";
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player") || c.name == "Boulders")
        {

            gameObject.Recycle();
        }
    }
}
