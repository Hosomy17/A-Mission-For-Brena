using UnityEngine;
using System.Collections;

public class ClassWave : ClassGeneric
{
    private Transform[] positions;
    public GameObject enemy;

    void Awake()
    {
        positions = GetComponentsInChildren<Transform>();
    }
    
    void OnEnable()
    {
        foreach(Transform it in positions)
        {
            enemy.Spawn(it);
        }
    }

    void OnDisable()
    {
        BehaviourPhysics.Move(gameObject, Vector2.one, 0f);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.name == "Destroy Wave")
        {
            BroadcastMessage("Kill");
        }
    }

    public void Kill()
    {
        gameObject.Recycle();
    }
}
