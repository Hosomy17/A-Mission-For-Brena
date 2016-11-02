using UnityEngine;
using System.Collections;

public static class BehaviourPhysics
{
    public static void Move(GameObject gameObject, Vector2 directions, float speed)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        if(directions.x != 0)
            rigidbody2D.velocity = new Vector2(directions.x * speed, rigidbody2D.velocity.y);
        if(directions.y != 0)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, directions.y * speed);
        
    }

    public static void Force(GameObject gameObject, Vector2 directions, float force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(directions * force);
    }

    public static void Torque(GameObject gameObject, float speed)
    {
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = speed;
    }
}
