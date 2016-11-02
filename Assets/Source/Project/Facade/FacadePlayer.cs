using UnityEngine;
using System.Collections;

public class FacadePlayer
{
    public static void Move(ClassPlayer classGeneric, Vector2 directions, float speed)
    {
        BehaviourPhysics.Move(classGeneric.gameObject, directions, speed);
    }

    public static void Idle(ClassGeneric classGeneric, Vector2 directions)
    {
        BehaviourPhysics.Move(classGeneric.gameObject, directions, 0);
    }
}
