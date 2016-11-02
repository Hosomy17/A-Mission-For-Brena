using UnityEngine;
using System.Collections;

public static class BehaviourAnimation
{
    public static void Play(GameObject gameObject, string animation)
    {
        gameObject.GetComponent<Animator>().Play(animation);
    }

    public static void Trigger(GameObject gameObject, string trigger)
    {
        gameObject.GetComponent<Animator>().SetTrigger(trigger);
    }

    public static void ResetTrigger(GameObject gameObject, string trigger)
    {
        gameObject.GetComponent<Animator>().ResetTrigger(trigger);
    }

    public static void Flip(GameObject gameObject, Vector2 directions)
    {
        SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        
        if (directions.x == 1)
            spr.flipX = false;
        else if (directions.x == -1)
            spr.flipX = true;

        if (directions.y == 1)
            spr.flipY = false;
        else if (directions.y == -1)
            spr.flipY = true;
    }
}
